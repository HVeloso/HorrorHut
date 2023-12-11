using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FollowerCamera : MonoBehaviour
{
    [Header("Follow Axis")]
    [SerializeField] private bool followXAxis;
    [SerializeField] private bool followYAxis;
    [SerializeField] private bool followZAxis;

    [Header("Axis Distance")]
    [SerializeField] private float distanceFromXAxis;
    [SerializeField] private float distanceFromYAxis;
    [SerializeField] private float distanceFromZAxis;

    private Transform playerTransform;
    private Vector3 newPosition;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            newPosition.x = followXAxis ? distanceFromXAxis + playerTransform.position.x : transform.position.x;
            newPosition.y = followYAxis ? distanceFromYAxis + playerTransform.position.y : transform.position.y;
            newPosition.z = followZAxis ? distanceFromZAxis + playerTransform.position.z : transform.position.z;

            transform.position = newPosition;
        }
    }
}
