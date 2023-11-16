using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCamera : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        CamerasManager.GetNewCamera(transform.GetChild(0).gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        CamerasManager.RemoveCamera(transform.GetChild(0).gameObject);
    }
}
