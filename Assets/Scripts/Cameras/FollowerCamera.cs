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

    [Header("Motion Delay")]
    [SerializeField][Range(0f, 20f)] private float followSpeed;

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
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        UpdateNewPosition();
        ClampPosition();

        float totalFollowSpeed = Time.deltaTime * Time.timeScale * followSpeed;
        transform.position = Vector3.Lerp(transform.position, newPosition, totalFollowSpeed);
    }

    private void UpdateNewPosition()
    {
        newPosition.x = ShouldFollowAxis(followXAxis, distanceFromXAxis, transform.position.x, playerTransform.position.x);
        newPosition.y = ShouldFollowAxis(followYAxis, distanceFromYAxis, transform.position.y, playerTransform.position.y);
        newPosition.z = ShouldFollowAxis(followZAxis, distanceFromZAxis, transform.position.z, playerTransform.position.z);
    }

    private float ShouldFollowAxis(bool shouldFollow, float distance, float currentPosition, float playerPosition)
    {
        return shouldFollow ? distance + playerPosition : currentPosition;
    }

    private void ClampPosition()
    {
        ClampAxisPosition(ref newPosition.x, minimumXLimit, maximumXLimit);
        ClampAxisPosition(ref newPosition.y, minimumYLimit, maximumYLimit);
        ClampAxisPosition(ref newPosition.z, minimumZLimit, maximumZLimit);
    }

    private void ClampAxisPosition(ref float position, float minLimit, float maxLimit)
    {
        if (minLimit != maxLimit)
            position = Mathf.Clamp(position, minLimit, maxLimit);
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
