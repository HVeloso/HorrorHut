using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerTankMovement : MonoBehaviour
{
    [Header("Forward speed settings")]
    [SerializeField] private float baseSpeed;
    [SerializeField] private float maximumSpeed;
    [SerializeField] private float accelerationRate;
    [SerializeField] private float timeToStartAccelerating;

    [Header("Rotation speed settings")]
    [SerializeField] private float rotationSpeed;
    [SerializeField][Range(0f, 1f)] private float walkingRotationRate;

    private float accelerationTimer = 0f;
    private float currentSpeed;

    private CharacterController characterController;
    private Vector3 movementInputVector;
    private float deltaTime = 0f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        currentSpeed = baseSpeed;
    }

    private void Update()
    {
        deltaTime = Time.deltaTime * Time.timeScale;

        ModifySpeed();
        MovePlayer();
    }

    private void ModifySpeed()
    {

        if (movementInputVector.y > 0) Accelerate();
        else Decelerate();
    }

    private void Accelerate()
    {
        accelerationTimer = accelerationTimer >= timeToStartAccelerating ? timeToStartAccelerating : accelerationTimer + deltaTime;
        if (accelerationTimer >= timeToStartAccelerating) currentSpeed = currentSpeed >= maximumSpeed ? maximumSpeed : currentSpeed + (accelerationRate * deltaTime);
    }

    private void Decelerate()
    {
        currentSpeed = baseSpeed;
        accelerationTimer = accelerationTimer <= 0f ? 0f : (accelerationTimer - (deltaTime * 1.5f)) * (1 + movementInputVector.y);
    }

    private void MovePlayer()
    {
        Vector3 movement = currentSpeed * movementInputVector.y * deltaTime * transform.forward;
        characterController.Move(movement);

        float rotation = movementInputVector.x * deltaTime * (movementInputVector.y != 0f ? rotationSpeed * walkingRotationRate : rotationSpeed);
        transform.Rotate(transform.up, rotation);
    }

    private void OnMovement(InputValue value)
    {
        movementInputVector = value.Get<Vector2>();
        movementInputVector.Normalize();
    }
}
