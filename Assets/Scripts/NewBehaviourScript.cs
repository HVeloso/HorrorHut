using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NewBehaviourScript : MonoBehaviour
{
    private CharacterController characterController;

    private NavMeshPath path;
    private Vector3 target;
    private int currentTargetIndex;

    [SerializeField] private float maxDistance = 0f;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    private void Awake()
    {
        path = new();
        target = transform.position;

        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        CreatePath();
        SetTargetOnPath();
        Walk();
    }

    private void CreatePath()
    {
        if (Input.GetMouseButtonDown(0))
        {
            path.ClearCorners();
            currentTargetIndex = 0;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Nav"))
                {
                    NavMesh.CalculatePath(transform.position, hit.point, NavMesh.AllAreas, path);
                }
            }
        }
    }

    private void SetTargetOnPath()
    {
        if (path.corners.Length <= 0) return;

        for (int idx = 0; idx < path.corners.Length; idx++)
        {
            if (currentTargetIndex == idx)
            {
                target = path.corners[idx];
                target.y = transform.position.y;

                break;
            }
        }
    }

    private void Walk()
    {
        if (Vector3.Distance(transform.position, target) <= maxDistance)
        {
            if (currentTargetIndex == path.corners.Length - 1)
            {
                currentTargetIndex = 0;
                path.ClearCorners();
            }
            else currentTargetIndex++;

            return;
        }

        transform.LookAt(target);

        characterController.Move(speed * Time.deltaTime * Time.timeScale * transform.forward);
    }

    private void OnDrawGizmos()
    {
        if (path == null) return;
        if (path.corners.Length <= 0) return;
        
        Vector3 previousPoint = Vector3.positiveInfinity;
        for (int idx = currentTargetIndex; idx < path.corners.Length; idx++)
        {
            if (previousPoint != Vector3.positiveInfinity)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(previousPoint, path.corners[idx]);
            }

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(path.corners[idx], 0.25f);

            previousPoint = path.corners[idx];
        }
    }
}
