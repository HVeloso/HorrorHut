using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColectableItem : MonoBehaviour, IInteractable
{
    [SerializeField]
    private UIObjectIconController uIObjectIconController;
    public UIObjectIconController UIObjectIconController {
        get { return uIObjectIconController; }
        set { uIObjectIconController = value; }
    }

    private void Awake()
    {
        UIObjectIconController.SetTargetObject(transform);
    }

    public void Interact()
    {
        Destroy(uIObjectIconController.gameObject);
        Destroy(gameObject);
    }
}
