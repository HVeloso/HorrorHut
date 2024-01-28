using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider))]
public class AnimatedInteractionObjectBehaviour : MonoBehaviour, IInteractable
{
    [Header("Parameters")]
    [SerializeField]
        private bool canInteractMoreThatOnce;
    [SerializeField]
    [Min(0f)]
        private float intervalBetweenInteractions = 0f;

    [SerializeField]
        private UIObjectIconController[] uIObjectIconController;

    UIObjectIconController[] IInteractable.UIObjectIconController
    {
        get { return uIObjectIconController; }
        set { uIObjectIconController = value; }
    }

    private bool canInteract = true;
    private Animator objectAnimator;

    private void Awake()
    {
        objectAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        foreach (var icon in uIObjectIconController)
            icon.SetTargetObject(transform);
    }

    public void Interact()
    {
        if (!canInteract) return;

        StartCoroutine(InteractionCooldown());
        RunObjectAnimation();
    }

    private void RunObjectAnimation()
    {
        if (!canInteractMoreThatOnce)
        {
            gameObject.layer = LayerMask.GetMask("Default");
            foreach (var icon in uIObjectIconController)
                Destroy(icon.gameObject);
        }

        objectAnimator.SetTrigger("RunAnimation");
    }

    private IEnumerator InteractionCooldown()
    {
        canInteract = false;

        yield return new WaitForSeconds(intervalBetweenInteractions);

        canInteract = true;
    }
}
