using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerPerceptionManager : MonoBehaviour
{
    private Vector2 movementInputVector;

    private InteractableItemAnimationController interactableAnimationItem;
    private List<GameObject> interactablesOnArea = new();
    private GameObject nearestItem;

    private void Update()
    {
        if (movementInputVector != Vector2.zero)
            SetNearestItem();
    }

    public void AddInteractableAnimationItem(InteractableItemAnimationController item)
    {
        if (interactableAnimationItem == null)
            interactableAnimationItem = item;
    }

    public void RemoveInteractableAnimationItem(InteractableItemAnimationController item)
    {
        if (interactableAnimationItem == item)
            interactableAnimationItem = null;
    }

    public void AddItemOnNearItensList(GameObject obj)
    {
        if (interactablesOnArea.Contains(obj)) return;
        interactablesOnArea.Add(obj);
    }

    public void RemoveItemOnNearItensList(GameObject obj)
    {
        if (!interactablesOnArea.Contains(obj)) return;
        interactablesOnArea.Remove(obj);
    }

    private void SetNearestItem()
    {
        if (interactablesOnArea.Count <= 0) return;
        if (interactableAnimationItem != null && nearestItem != null)
        {
            nearestItem.GetComponent<PickableItem>().ChangeIcon();
            nearestItem = null;
            return;
        }

        Vector3 playerPosition = transform.position;

        nearestItem = nearestItem == null ? interactablesOnArea[0] : nearestItem;
        GameObject newNearestItem = nearestItem;
        float currentNearestDistance = Vector3.Distance(playerPosition, nearestItem.transform.position);

        float distance;
        foreach (GameObject obj in interactablesOnArea)
        {
            distance = Vector3.Distance(playerPosition, obj.transform.position);

            if (distance < currentNearestDistance)
            {
                newNearestItem = obj;
            }
        }

        if (newNearestItem == nearestItem) return;
        else
        {
            nearestItem.GetComponent<PickableItem>().ChangeIcon();

            nearestItem = newNearestItem;
            nearestItem.GetComponent<PickableItem>().ChangeIcon();
        }
    }

    private void OnMovement(InputValue value)
    {
        movementInputVector = value.Get<Vector2>();
        movementInputVector.Normalize();
    }

    private void OnInteract(InputValue value)
    {
        if (value.isPressed)
        {
            if (interactableAnimationItem != null)
            {
                interactableAnimationItem.Interact();
            }
            else if (nearestItem != null)
            {
                RemoveItemOnNearItensList(nearestItem);
                nearestItem.GetComponent<IInteractable>().Interact();
                nearestItem = null;
                SetNearestItem();
            }
        }
    }
}
