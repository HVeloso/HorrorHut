using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FollowerCamera : MonoBehaviour
{
    #region Properties

    [Header("Follow Axis")]
    [SerializeField] private bool followXAxis;
    [SerializeField] private bool followYAxis;
    [SerializeField] private bool followZAxis;

    [Header("Axis Distance")]
    [SerializeField] private float distanceFromXAxis;
    [SerializeField] private float distanceFromYAxis;
    [SerializeField] private float distanceFromZAxis;

    [Header("Axis Position Limiter (World Position)")]
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
    private Vector3 newPosition;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        newPosition.x = followXAxis ? distanceFromXAxis + playerTransform.position.x : transform.position.x;
        newPosition.y = followYAxis ? distanceFromYAxis + playerTransform.position.y : transform.position.y;
        newPosition.z = followZAxis ? distanceFromZAxis + playerTransform.position.z : transform.position.z;

        if (minimumXLimit != 0 && maximumXLimit != 0)
            newPosition.x = Mathf.Clamp(newPosition.x, minimumXLimit, maximumXLimit);

        if (minimumYLimit != 0 && maximumYLimit != 0)
            newPosition.y = Mathf.Clamp(newPosition.y, minimumYLimit, maximumYLimit);

        if (minimumZLimit != 0 && maximumZLimit != 0)
            newPosition.z = Mathf.Clamp(newPosition.z, minimumZLimit, maximumZLimit);

        transform.position = newPosition;
    }

#if UNITY_EDITOR
    [ContextMenu(nameof(GetCameraWorldPosition))]
    private void GetCameraWorldPosition()
    {
        Debug.Log($"Positions: X: {transform.position.x} | Y: {transform.position.y} | Z: {transform.position.z}");
    }

    [ContextMenu(nameof(GetDistanceFromPlayer))]
    private void GetDistanceFromPlayer()
    {
        Vector3 distanceFormPlayer = transform.position - playerTransform.position;

        Debug.Log($"Distances: X: {distanceFormPlayer.x} | Y: {distanceFormPlayer.y} | Z: {distanceFormPlayer.z}");
    }
#endif
}
