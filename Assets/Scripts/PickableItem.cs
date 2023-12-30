using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PickableItem : MonoBehaviour, IInteractable
{
    public virtual void Interact() { }

    public virtual void ChangeIcon() { }
}
