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
    private Transform referenceTransform;
    private Vector3 newAngles;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        referenceTransform = Instantiate(new GameObject("Reference"), transform).transform;
    }

    private void LateUpdate()
    {
        referenceTransform.LookAt(playerTransform.position);

        newAngles.x = followXAxis ? referenceTransform.eulerAngles.x : transform.eulerAngles.x;
        newAngles.y = followYAxis ? referenceTransform.eulerAngles.y : transform.eulerAngles.y;
        newAngles.z = followZAxis ? referenceTransform.eulerAngles.z : transform.eulerAngles.z;

        if (minimumXLimit != 0 && maximumXLimit != 0)
            newAngles.x = ClampRotation(newAngles.x, minimumXLimit, maximumXLimit);

        if (minimumYLimit != 0 && maximumYLimit != 0)
            newAngles.y = ClampRotation(newAngles.y, minimumYLimit, maximumYLimit);

        if (minimumZLimit != 0 && maximumZLimit != 0)
            newAngles.z = ClampRotation(newAngles.z, minimumZLimit, maximumZLimit);

        transform.eulerAngles = newAngles;
    }

    private float ClampRotation(float value, float minimum, float maximum)
    {
        float centralOppositeValue;

        if (minimum < maximum)
        {
            centralOppositeValue = (minimum + maximum) / 2 + 180;
            if (centralOppositeValue > 360) centralOppositeValue -= 360;

            if (value < minimum) value = minimum;
            else if (value > maximum) value = maximum;
        }
        else
        {
            centralOppositeValue = (minimum + maximum) / 2;

            if (value < minimum && value > centralOppositeValue) value = minimum;
            else if (value > maximum && value < centralOppositeValue) value = maximum;
        }

        return value;
    }

#if UNITY_EDITOR
    [ContextMenu(nameof(GetCameraWorldRotation))]
    private void GetCameraWorldRotation()
    {
        Debug.Log($"X: {transform.eulerAngles.x} | Y: {transform.eulerAngles.y} | Z: {transform.eulerAngles.z}");
    }
#endif
}