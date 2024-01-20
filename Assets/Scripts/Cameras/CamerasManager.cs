using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public static class CamerasManager
{
    private static PlayerStateMachineContext playerStatesManager;
    private static PlayerMovement playerMovement;
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
        playerStatesManager ??= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateMachineContext>();
        playerMovement ??= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        CurrentCamera.SetActive(true);
        playerStatesManager.UpdateCameraReference(CurrentCamera.transform);
        playerMovement.UpdateCameraReference(CurrentCamera.transform);
    }
}
