using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : PickableItem
{
    public override void Interact()
    {
        Destroy(gameObject);
    }
}
