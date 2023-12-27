using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ObserverCamera : MonoBehaviour
{
    #region Properties

    [Header("Follow Axis")]
    [SerializeField] private bool followXAxis;
    [SerializeField] private bool followYAxis;
    [SerializeField] private bool followZAxis;

    [Header("Rotate Delay")]
    [SerializeField] [Range(0f, 20f)] private float rotateSpeed;

    [Header("Axis Rotation Limiter (World Rotation)")]
    [Header("Minimum Limit")]
    [SerializeField] private float minimumXLimit;
    [SerializeField] private float minimumYLimit;
    [SerializeField] private float minimumZLimit;

    [Header("Maximum Limit")]
    [SerializeField] private float maximumXLimit;
    [SerializeField] private float maximumYLimit;
    [SerializeField] private float maximumZLimit;

    #endregion

    private Transform playerTransform;
    private Vector3 newAngles;

    private Vector3 directionToLook;
    private Quaternion referenceQuaternion;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        UpdateCameraRotation();
    }

    private void UpdateCameraRotation()
    {
        directionToLook = playerTransform.position - transform.position;
        referenceQuaternion = Quaternion.LookRotation(directionToLook);

        newAngles.x = followXAxis ? referenceQuaternion.eulerAngles.x : transform.eulerAngles.x;
        newAngles.y = followYAxis ? referenceQuaternion.eulerAngles.y : transform.eulerAngles.y;
        newAngles.z = followZAxis ? referenceQuaternion.eulerAngles.z : transform.eulerAngles.z;

        ClampRotationAngles();

        float totalRotateSpeed = Time.deltaTime * Time.timeScale * rotateSpeed;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newAngles), totalRotateSpeed);
    }

    private void ClampRotationAngles()
    {
        ClampAxisRotation(ref newAngles.x, minimumXLimit, maximumXLimit);
        ClampAxisRotation(ref newAngles.y, minimumYLimit, maximumYLimit);
        ClampAxisRotation(ref newAngles.z, minimumZLimit, maximumZLimit);
    }

    private float ClampAxisRotation(ref float currentAngle, float minimum, float maximum)
    {
        // Limita o ângulo atual entre os valores mínimo e máximo fornecidos.
        // Caso o ângulo esteja fora dos limites, realiza ajustes para mantê-lo dentro do intervalo desejado.

        float centralOppositeValue;

        if (minimum < maximum)
        {
            centralOppositeValue = (minimum + maximum) / 2 + 180;
            if (centralOppositeValue > 360) centralOppositeValue -= 360;

            if (currentAngle < minimum) currentAngle = minimum;
            else if (currentAngle > maximum) currentAngle = maximum;
        }
        else
        {
            centralOppositeValue = (minimum + maximum) / 2;

            if (currentAngle < minimum && currentAngle > centralOppositeValue) currentAngle = minimum;
            else if (currentAngle > maximum && currentAngle < centralOppositeValue) currentAngle = maximum;
        }

        return currentAngle;
    }

#if UNITY_EDITOR
    [ContextMenu(nameof(GetCameraWorldRotation))]
    private void GetCameraWorldRotation()
    {
        Debug.Log($"X: {transform.eulerAngles.x} | Y: {transform.eulerAngles.y} | Z: {transform.eulerAngles.z}");
    }
#endif
}