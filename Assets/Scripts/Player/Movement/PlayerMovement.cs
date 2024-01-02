using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Forward speed settings")]
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float runningSpeed;

    private NavMeshAgent navMeshAgent;
    private Transform newCameraReference;
    private Transform currentCameraReference;

    private Vector2 movementInputVector;
    private Vector3 forwardRelativeToCamera;
    private Vector3 rightRelativeToCamera;
    private Vector3 movimentionDirection;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        UpdatePlayerSpeed(walkingSpeed);
        StartCoroutine(FirstCameraReference());
    }

    private void OnMovement(InputValue value)
    {
        movementInputVector = value.Get<Vector2>();

        if (movementInputVector.magnitude <= 0)
            SyncPlayerMovementSettings();
        else
            MovePlayer();
    }

    private void OnRun(InputValue value)
    {
        if (value.isPressed)
            UpdatePlayerSpeed(navMeshAgent.speed == runningSpeed ? walkingSpeed : runningSpeed);
    }

    public void UpdateCameraReference(Transform cameraReference)
    {
        newCameraReference = cameraReference;
    }

    private void SyncPlayerMovementSettings()
    {
        if (newCameraReference != null && newCameraReference != currentCameraReference)
            currentCameraReference = newCameraReference;

        if (navMeshAgent.speed == runningSpeed)
            UpdatePlayerSpeed(walkingSpeed);

        navMeshAgent.ResetPath();
    }

    private void UpdatePlayerSpeed(float speed)
    {
        navMeshAgent.speed = speed;
        navMeshAgent.acceleration = speed;
    }

    private void MovePlayer()
    {
        forwardRelativeToCamera = currentCameraReference.forward * movementInputVector.y;
        rightRelativeToCamera = currentCameraReference.right * movementInputVector.x;
        movimentionDirection = forwardRelativeToCamera + rightRelativeToCamera;
        movimentionDirection.Normalize();

        Vector3 destination = transform.position + movimentionDirection;

        navMeshAgent.SetDestination(destination);
    }

    private IEnumerator FirstCameraReference()
    {
        yield return new WaitUntil(() => newCameraReference != null);
        currentCameraReference = newCameraReference;
    }
}
