using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerAnimationContoller : InteractableItemAnimationController
{
    [SerializeField] private Transform drawerPivot;

    private bool open = false;

    private float angle = -.35f;
    private float openAngle = -.7f;
    private float closeAngle = -.35f;
    private float targetAngle = -.35f;

    private float time = .8f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow)) Interact();

        angle = Mathf.MoveTowards(angle, targetAngle, time * Time.deltaTime * Time.timeScale);
        drawerPivot.localPosition = new Vector3(0, .8f, angle);
    }

    public override void Interact()
    {
        targetAngle = open ? closeAngle : openAngle;
        open = !open;
    }
}