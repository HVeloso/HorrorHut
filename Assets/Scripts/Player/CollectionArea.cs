using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollectionArea : MonoBehaviour
{
    private PlayerPerceptionManager playerPerception;

    private void Awake()
    {
        playerPerception = transform.parent.parent.GetComponent<PlayerPerceptionManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PickableItem>(out PickableItem item))
        {
            playerPerception.AddItemOnNearItensList(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PickableItem>(out PickableItem item))
        {
            playerPerception.RemoveItemOnNearItensList(other.gameObject);
        }
    }
}
