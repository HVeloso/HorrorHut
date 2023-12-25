using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerInput))]
public class PlayerQuickTurnFeature : MonoBehaviour
{
    [Header("Quick turn settings")]
    [SerializeField] private float rotationMovementDuration;
    [SerializeField] private float quickTurnCooldown;
    private bool canRotate = true;

    private IEnumerator RotatePlayer(Vector3 startRotation, Vector3 endRotation, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            float interpolationValue = time / duration;
            transform.eulerAngles = Vector3.Lerp(startRotation, endRotation, interpolationValue);
            time += Time.deltaTime * Time.timeScale;
            yield return null;
        }
        transform.eulerAngles = endRotation;

        yield return new WaitForSeconds(quickTurnCooldown);
        canRotate = true;
    }

    private void OnFastTurn(InputValue value)
    {
        if (value.isPressed && canRotate)
        {
            canRotate = false;
            
            Vector3 currentRotation = transform.eulerAngles;
            float targetRotationY = currentRotation.y + 180f;

            StartCoroutine(RotatePlayer(transform.eulerAngles, new Vector3(0f, targetRotationY, 0f), rotationMovementDuration));
        }
    }
}
