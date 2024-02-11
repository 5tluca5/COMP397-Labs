using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    COMP397Labs inputs;

    [SerializeField] int index = 0;
    [SerializeField] CinemachineVirtualCamera currentCamera;
    [SerializeField] List<CinemachineVirtualCamera> virtualCameras = new List<CinemachineVirtualCamera>();

    private void Awake()
    {
        InitCameraPriorities();

        inputs = new COMP397Labs();

        inputs.Player.Camera.performed += context => MoveCamera(context.ReadValue<float>());
    }

    private void OnEnable() => inputs.Enable();

    private void OnDisable() => inputs.Disable();

    private void InitCameraPriorities()
    {
        foreach (var cam in virtualCameras)
        {
            cam.Priority = 0;
        }

        currentCamera = virtualCameras.First();
        currentCamera.Priority = 10;
    }

    private void MoveCamera(float value)
    {
        Debug.Log($"Camera changed value{value}");

        index += (int)value;

        currentCamera.Priority = 0;
        currentCamera = virtualCameras[Mathf.Abs(index % virtualCameras.Count)];
        currentCamera.Priority = 10;
    }

}
