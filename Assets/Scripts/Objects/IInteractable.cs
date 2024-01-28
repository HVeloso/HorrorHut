using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public UIObjectIconController[] UIObjectIconController { get; set; }

    public void Interact();
}
