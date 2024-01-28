using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntemHolderObjectFeature : MonoBehaviour
{
    [SerializeField] private bool hideItemAtStart;
    [SerializeField] private GameObject item;

    private void Awake()
    {
        if (hideItemAtStart)
        {
            item.SetActive(false);
        }

        (item.GetComponent<IInteractable>() as MonoBehaviour).enabled = false;
    }

    public void EnableItem()
    {
        item.SetActive(true);
    }

    public void EnableItemCollectScript()
    {
        (item.GetComponent<IInteractable>() as MonoBehaviour).enabled = true;
    }
}
