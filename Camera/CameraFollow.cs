using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private CameraLocation cameraLocation;

    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        cameraLocation = player.GetComponentInChildren<CameraLocation>();
        UpdateCameraLocation();
    }

    public void UpdateCameraLocation()
    {
        gameObject.transform.SetParent(cameraLocation.transform);
        gameObject.transform.position = cameraLocation.transform.position;
        gameObject.transform.rotation = cameraLocation.transform.rotation;
    }
}
