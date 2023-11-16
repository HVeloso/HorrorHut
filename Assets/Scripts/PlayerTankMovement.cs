using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTankMovement : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float turnSpeed = 180f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 moveDir;

        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);
        moveDir = Input.GetAxis("Vertical") * speed * transform.forward;

        controller.Move(moveDir * Time.deltaTime - Vector3.up * .1f);
    }
}
