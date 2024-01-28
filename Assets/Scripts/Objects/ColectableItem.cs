using UnityEngine;

public class ColectableItem : MonoBehaviour, IInteractable
{
    [SerializeField]
        private UIObjectIconController[] uIObjectIconController;
    public UIObjectIconController[] UIObjectIconController {
        get { return uIObjectIconController; }
        set { uIObjectIconController = value; }
    }

    private void OnEnable()
    {
        foreach (var icon in uIObjectIconController)
            icon.SetTargetObject(transform);
    }

    public void Interact()
    {
        AddMeOnInventory();
    }

    private void AddMeOnInventory()
    {
        foreach (var icon in uIObjectIconController)
            Destroy(icon.gameObject);

        Destroy(gameObject);
    }
}
