using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Image))]
public class UIObjectIconController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private Vector3 offset;
    [SerializeField] private Sprite[] sprites;
    
    private Transform target;
    private Image currentImage;

    private static Canvas objectIconCanvas;

    public delegate void OnCurrentCameraActivated();
    public static event OnCurrentCameraActivated CurrentCameraActivated;

    private void Awake()
    {
        objectIconCanvas ??= GameObject.FindGameObjectWithTag("Object Icon UI").GetComponent<Canvas>();
        currentImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        CurrentCameraActivated += UpdateImagePositionAndRotation;
    }

    private void OnDisable()
    {
        CurrentCameraActivated -= UpdateImagePositionAndRotation;
    }

    public static void UpdateUIObjectIconCameraReference()
    {
        objectIconCanvas.worldCamera = CamerasManager.CurrentCamera.GetComponent<Camera>();
        CurrentCameraActivated?.Invoke();
    }

    public void SetTargetObject(Transform targetObject)
    {
        target = targetObject;
        UpdateImagePositionAndRotation();
    }

    public void SetImageAsDetected(Transform playerPosition, float maxDistante)
    {
        currentImage.sprite = sprites[0];
        currentImage.enabled = true;

        StartCoroutine(WaitForExitDetectionZone(playerPosition, maxDistante));
    }

    public void SetImageAsNearest(Transform playerPosition, float maxDistante)
    {
        currentImage.sprite = sprites[1];

        StartCoroutine(WaitForExitInteractionZone(playerPosition, maxDistante));
    }

    private void UpdateImagePositionAndRotation()
    {
        if (CamerasManager.CurrentCamera == null) return;
        if (target == null) return;

        Vector3 pos = target.position + offset;
        transform.position = pos;

        transform.LookAt(CamerasManager.CurrentCamera.transform);
    }

    private IEnumerator WaitForExitDetectionZone(Transform playerPosition, float maxDistante)
    {
        yield return new WaitUntil(() => Vector3.Distance(playerPosition.position, transform.position) > maxDistante);

        currentImage.enabled = false;
    }

    private IEnumerator WaitForExitInteractionZone(Transform playerPosition, float maxDistante)
    {
        yield return new WaitUntil(() => Vector3.Distance(playerPosition.position, transform.position) > maxDistante);

        currentImage.sprite = sprites[0];
    }
}
