using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public static class CamerasManager
{
    private static PlayerMovement playerMovementScript;
    private static readonly List<GameObject> cameras = new();
    public static GameObject CurrentCamera { get; private set; }

    public static void GetNewCamera(GameObject newCam)
    {
        cameras.Insert(0, newCam);

        CurrentCamera = newCam;

        if (cameras.Count > 1) cameras[1].SetActive(false);
        ActiveCurrentCamera();
    }

    public static void RemoveCamera(GameObject camera)
    {
        if (camera == cameras[0])
        {
            CurrentCamera = cameras[1];

            cameras[0].SetActive(false);
            ActiveCurrentCamera();
        }

        cameras.Remove(camera);
    }

    private static void ActiveCurrentCamera()
    {
        if (playerMovementScript == null) playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        CurrentCamera.SetActive(true);
        playerMovementScript.UpdateCameraReference(CurrentCamera.transform);
    }
}
