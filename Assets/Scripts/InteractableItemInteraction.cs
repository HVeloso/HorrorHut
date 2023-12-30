using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InteractableItemInteraction : MonoBehaviour
{
    private PlayerPerceptionManager playerPerception;

    private void Awake()
    {
        playerPerception = transform.parent.parent.GetComponent<PlayerPerceptionManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out InteractableItemAnimationController item))
        {
            playerPerception.AddInteractableAnimationItem(item);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out InteractableItemAnimationController item))
        {
            playerPerception.RemoveInteractableAnimationItem(item);
        }
    }
}
