using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;
using static UnityEngine.GraphicsBuffer;

public class PlayerObjectInteractactFeature : MonoBehaviour
{
    [Header("Visual Debug")]
    [SerializeField] private bool ShowGizmos = false;

    [Space(1)]

    [Header("Parameters")]
    [SerializeField] private float detectRadius;
    [SerializeField] private float interactRadius;
    [SerializeField] private float interactAngle;

    private GameObject nearestItem;

    private void FixedUpdate()
    {
        DetectAndSetNearestItem();
    }

    private void DetectAndSetNearestItem()
    {
        DetectItens();
        nearestItem = GetNearestItem();
    }

    private GameObject GetNearestItem()
    {
        Collider[] objectsOnInteractionRange = Physics.OverlapSphere(transform.position, interactRadius, LayerMask.GetMask("Interactable"));
        List<GameObject> objectsOnInteractionArea = new();

        if (objectsOnInteractionRange.Length <= 0) return null;

        foreach (Collider objectInRange in objectsOnInteractionRange)
        {
            if (IsObjectInInteractionAngle(objectInRange.transform.position) && !VerifyWallObstruction(objectInRange.transform.position))
            {
                objectsOnInteractionArea.Add(objectInRange.gameObject);
            }
        }

        if (objectsOnInteractionArea.Count <= 0) return null;

        objectsOnInteractionArea = objectsOnInteractionArea.OrderBy(item => Vector3.Distance(item.transform.position, transform.position)).ToList();
        objectsOnInteractionArea[0].GetComponent<IInteractable>().UIObjectIconController.SetImageAsNearest(transform, detectRadius);

        return objectsOnInteractionArea[0];
    }

    private bool IsObjectInInteractionAngle(Vector3 objectPosition)
    {
        objectPosition.y = transform.position.y;

        Vector3 directionToTarget = objectPosition - transform.position;

        return Vector3.Angle(transform.forward, directionToTarget) <= interactAngle / 2;
    }

    private void DetectItens()
    {
        Collider[] objectsOnDetectionRange = Physics.OverlapSphere(transform.position, detectRadius, LayerMask.GetMask("Interactable"));

        for (int idx = 0; idx < objectsOnDetectionRange.Length; idx++)
        {
            Transform itemTransform = objectsOnDetectionRange[idx].transform;

            if (!VerifyWallObstruction(itemTransform.position))
            {
                IInteractable itemIInteractable = itemTransform.GetComponent<IInteractable>();

                itemIInteractable.UIObjectIconController.SetImageAsDetected(transform, detectRadius);
            }
        }
    }

    private bool VerifyWallObstruction(Vector3 position)
    {
        Ray ray = new(transform.position, position - transform.position);
        float distance = Vector3.Distance(position, transform.position) - .1f;

        return Physics.Raycast(ray, distance, LayerMask.GetMask("Vision Blocker"));
    }

    private void OnDrawGizmos()
    {
        if (!ShowGizmos) return;

        // Raio da detecção de itens
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectRadius);

        // Raio da interação de itens
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactRadius);

        // Ângulo da interação de itens
        float startAngle = (interactAngle / 2 + 90 - transform.eulerAngles.y) * Mathf.Deg2Rad;
        float endAngle = startAngle - (interactAngle * Mathf.Deg2Rad);
        float currentAngle = startAngle;

        while (currentAngle > endAngle)
        {
            currentAngle -= 5f * Mathf.Deg2Rad;
            if (currentAngle < endAngle) currentAngle = endAngle;
            
            float targetX = transform.position.x + interactRadius * Mathf.Cos(currentAngle);
            float targetZ = transform.position.z + interactRadius * Mathf.Sin(currentAngle);

            Vector3 targetPosition = new(targetX, transform.position.y, targetZ);

            Gizmos.DrawLine(transform.position, targetPosition);
        }

        // Distância do item mais próximo
        if (nearestItem != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, nearestItem.transform.position);
        }
    }

    private void OnInteract(InputValue value)
    {
        if (nearestItem != null && value.isPressed)
        {
            nearestItem.GetComponent<IInteractable>().Interact();
            DetectAndSetNearestItem();
        }
    }
}
