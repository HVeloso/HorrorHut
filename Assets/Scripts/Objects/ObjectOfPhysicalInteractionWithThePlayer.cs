using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOfPhysicalInteractionWithThePlayer : MonoBehaviour, IInteractable
{
    [Header("Parameters")]
    [SerializeField] [Min(0f)]
        private float intervalBetweenInteractions = 0f;

    [SerializeField]
        private UIObjectIconController[] uIObjectIconController;

    UIObjectIconController[] IInteractable.UIObjectIconController
    {
        get { return uIObjectIconController; }
        set { uIObjectIconController = value; }
    }

    public delegate void OnInteractHandler();
    public event OnInteractHandler OnInteracted;

    private bool canInteract = true;

    private void OnEnable()
    {
        foreach (var icon in uIObjectIconController)
            icon.SetTargetObject(transform);
    }

    public void Interact()
    {
        if (!canInteract) return;

        StartCoroutine(InteractionCooldown());

        OnInteracted?.Invoke();
    }

    private IEnumerator InteractionCooldown()
    {
        canInteract = false;

        yield return new WaitForSeconds(intervalBetweenInteractions);

        canInteract = true;
    }
}
