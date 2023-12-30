using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimationController : InteractableItemAnimationController
{
    [SerializeField] private Transform doorPivot;

    private bool open = false;

    private float angle = 0f;
    private float openAngle = 90f;
    private float closeAngle = 0f;
    private float targetAngle = 0f;

    private float time = 80f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow)) Interact();

        angle = Mathf.MoveTowards(angle, targetAngle, time * Time.deltaTime * Time.timeScale);
        doorPivot.rotation = Quaternion.Euler(0, angle, 0);
    }

    public override void Interact()
    {
        targetAngle = open ? closeAngle : openAngle;
        open = !open;
    }
}
