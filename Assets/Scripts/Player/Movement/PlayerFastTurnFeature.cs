using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerInput))]
public class PlayerFastTurnFeature : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;

    private bool isRotating = false;
    private float currentRotate = 0f;
    private float rotateInThisFrame;

    private void Update()
    {
        if (currentRotate > 0f)
        {
            rotateInThisFrame = rotateSpeed * Time.deltaTime * Time.timeScale;

            if (rotateInThisFrame > currentRotate)
            {
                rotateInThisFrame -= currentRotate;    
                currentRotate = 0f;
                isRotating = false;
            }
            else
            {
                currentRotate -= rotateInThisFrame;
            }

            transform.Rotate(transform.up, rotateInThisFrame);
        }
    }

    private void OnFastTurn(InputValue value)
    {
        if (value.isPressed && !isRotating)
        {
            isRotating = true;
            currentRotate = 180f;
        }
    }
}