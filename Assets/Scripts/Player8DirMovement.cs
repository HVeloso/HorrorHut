using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player8DirMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody rigidbody;

    private float newXPosition;
    private float newZPosition;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        newXPosition = speed * Time.deltaTime * Input.GetAxis("Horizontal");
        newZPosition = speed * Time.deltaTime * Input.GetAxis("Vertical");

        rigidbody.MovePosition(transform.position + new Vector3(newXPosition, 0f, newZPosition));
    }
}
