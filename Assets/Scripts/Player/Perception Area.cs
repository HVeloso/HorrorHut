using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PerceptionArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PickableItem>(out PickableItem item))
        {
            item.ChangeIcon();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PickableItem>(out PickableItem item))
        {
            item.ChangeIcon();
        }
    }
}
