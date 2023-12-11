using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ObserverCamera : MonoBehaviour
{
    [Header("Follow Axis")]
    [SerializeField] private bool followXAxis;
    [SerializeField] private bool followYAxis;
    [SerializeField] private bool followZAxis;

    private Transform playerTransform;
    private Vector3 positionToLook;
    private Vector3 refPos;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        refPos = transform.position + transform.forward;
    }

    private void Update()
    {
        positionToLook.x = followXAxis ? playerTransform.position.x : refPos.x;
        positionToLook.y = followYAxis ? playerTransform.position.y : refPos.y;
        positionToLook.z = followZAxis ? playerTransform.position.z : refPos.z;

        transform.LookAt(positionToLook);
    }
}