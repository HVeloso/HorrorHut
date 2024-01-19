using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Forward speed settings")]
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float runningSpeed;
    [SerializeField] private float gravityMultiplier = 3f;
    private float currentSpeed;
    private float gravity = -9.81f;
    private float gravityVelocity;

    private float deltaTime;

    private InputAction moveAction;

    private CharacterController characterController;
    private Transform newCameraReference;
    private Transform currentCameraReference;

    private Vector2 movementInputVector;
    private Vector3 forwardRelativeToCamera;
    private Vector3 rightRelativeToCamera;
    private Vector3 movimentionDirection;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        moveAction = GetComponent<PlayerInput>().actions.FindAction("Movement");
    }

    private void Start()
    {
        UpdatePlayerSpeed(walkingSpeed);
        StartCoroutine(GetFirstCameraReference());
    }

    private void FixedUpdate()
    {
        deltaTime = Time.deltaTime * Time.timeScale;

        MovePlayer();
    }

    public void UpdateCameraReference(Transform cameraReference)
    {
        newCameraReference = cameraReference;
    }

    private void MovePlayer()
    {
        movementInputVector = moveAction.ReadValue<Vector2>();
        movimentionDirection = Vector3.zero;

        if (movementInputVector.magnitude <= 0)
        {
            SyncPlayerMovementSettings();
        }
        else
        {
            ApplyMovement();
        }

        ApplyGravity();

        characterController.Move(currentSpeed * deltaTime * movimentionDirection);
    }

    private void ApplyGravity()
    {
        if (characterController.isGrounded)
        {
            gravityVelocity = -1.0f;
        }
        else
        {
            gravityVelocity += gravity * gravityMultiplier * deltaTime;
        }

        movimentionDirection.y = gravityVelocity;
    }

    private void ApplyMovement()
    {
        forwardRelativeToCamera = currentCameraReference.forward * movementInputVector.y;
        rightRelativeToCamera = currentCameraReference.right * movementInputVector.x;
        movimentionDirection = forwardRelativeToCamera + rightRelativeToCamera;
        movimentionDirection.Normalize();
    }

    private void SyncPlayerMovementSettings()
    {
        if (newCameraReference != null && newCameraReference != currentCameraReference)
            currentCameraReference = newCameraReference;

        if (currentSpeed == runningSpeed)
            UpdatePlayerSpeed(walkingSpeed);
    }

    private void UpdatePlayerSpeed(float speed)
    {
        currentSpeed = speed;
    }

    private IEnumerator GetFirstCameraReference()
    {
        yield return new WaitUntil(() => newCameraReference != null);
        currentCameraReference = newCameraReference;
    }

    private void OnRun(InputValue value)
    {
        if (value.isPressed)
            UpdatePlayerSpeed(currentSpeed == runningSpeed ? walkingSpeed : runningSpeed);
    }
}
