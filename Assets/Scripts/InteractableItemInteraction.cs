using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InteractableItemInteraction : MonoBehaviour
{
    private PlayerPerceptionManager playerPerception;
    private Transform perceptionCenterTransform;

    private void Awake()
    {
        playerPerception = transform.parent.parent.GetComponent<PlayerPerceptionManager>();
        perceptionCenterTransform = transform.parent.transform;
    }

    private void OnTriggerStay(Collider other)
    {
        Vector3 dir = (perceptionCenterTransform.position - other.transform.position);
        dir.Normalize();

        if (Physics.Raycast(perceptionCenterTransform.position, dir, out RaycastHit info, 2.75f))
        {
            if (other.transform.TryGetComponent(out InteractableItemAnimationController item))
            {
                Debug.DrawLine(perceptionCenterTransform.position, perceptionCenterTransform.position + perceptionCenterTransform.forward * 2.75f, Color.red, .01f);

                playerPerception.AddInteractableAnimationItem(item);
            }
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
