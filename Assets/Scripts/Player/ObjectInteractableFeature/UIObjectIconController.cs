using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectIconController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private Vector3 offset;
    [SerializeField] private Sprite[] sprites;
    
    private Transform target;
    private Image currentImage;

    private static Camera currentCamera;
    private static Canvas objectIconCanvas;

    public delegate void OnCurrentCameraActivated();
    public static event OnCurrentCameraActivated currentCameraActivated;

    private void Awake()
    {
        objectIconCanvas ??= GameObject.FindGameObjectWithTag("Object Icon UI").GetComponent<Canvas>();
        currentImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        currentCameraActivated += UpdateImagePositionAndRotation;
    }

    private void OnDisable()
    {
        currentCameraActivated -= UpdateImagePositionAndRotation;
    }

    public static void UpdateUIObjectIconCameraReference(GameObject newCamera)
    {
        currentCamera = newCamera.GetComponent<Camera>();
        objectIconCanvas.worldCamera = currentCamera;

        currentCameraActivated?.Invoke();
    }

    public void SetTargetObject(Transform targetObject)
    {
        target = targetObject;
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
        if (currentCamera == null) return;

        Vector3 pos = target.position + offset;
        transform.position = pos;

        transform.LookAt(currentCamera.transform);
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
