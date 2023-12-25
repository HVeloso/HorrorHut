using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerFastTurnFeature : MonoBehaviour
{
    [SerializeField] private float rotationDuration;
    [SerializeField] private float fastTurnCooldown;
    private bool isAlreadyRotating = false;

    private void OnFastTurn(InputValue value)
    {
        if (value.isPressed && !isAlreadyRotating)
        {
            isAlreadyRotating = true;
            float newRotationY = transform.eulerAngles.y + 180f;

            StartCoroutine(FastTurn(transform.eulerAngles, new Vector3(0f, newRotationY, 0f), rotationDuration));
        }
    }

    private IEnumerator FastTurn(Vector3 startRotation, Vector3 endRotation, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            float t = time / duration;
            transform.eulerAngles = Vector3.Lerp(startRotation, endRotation, t);
            time += Time.deltaTime * Time.timeScale;
            yield return null;
        }
        transform.eulerAngles = endRotation;

        yield return new WaitForSeconds(fastTurnCooldown);
        isAlreadyRotating = false;
    }
}
