using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerTankMovement : MonoBehaviour
{
    [Header("Forward speed setting")]
    [SerializeField] private float playerSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float timeToStartAccelerate;

    [Header("Rotation speed setting")]
    [SerializeField] private float rotateSpeed;
    [SerializeField][Range(0f, 1f)] private float rateOfRotationWhileWalking;

    private CharacterController characterController;
    private Vector3 movementIput;

    private float timeRunning = 0f;
    private float currentSpeed;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        currentSpeed = playerSpeed;
    }

    private void Update()
    {
        ModifyVelocity();
        PlayerMovement();
    }

    private void ModifyVelocity()
    {

        if (movementIput.y > 0)
        {
            timeRunning = timeRunning >= timeToStartAccelerate ? timeToStartAccelerate : timeRunning + (Time.deltaTime * Time.timeScale);
            if (timeRunning >= timeToStartAccelerate) currentSpeed = currentSpeed >= maxSpeed ? maxSpeed : currentSpeed + (acceleration * Time.deltaTime * Time.timeScale);
        }
        else
        {
            currentSpeed = playerSpeed;
            timeRunning = timeRunning <= 0f ? 0f : (timeRunning - (Time.deltaTime * Time.timeScale * 1.5f)) * (1 + movementIput.y);
        }
    }

    private void PlayerMovement()
    {
        characterController.Move(currentSpeed * movementIput.y * Time.deltaTime * transform.forward);
        transform.Rotate(transform.up, movementIput.x * Time.deltaTime * (movementIput.y != 0f ? rotateSpeed * rateOfRotationWhileWalking : rotateSpeed));
    }

    private void OnMovement(InputValue value)
    {
        movementIput = value.Get<Vector2>();
    }
}
