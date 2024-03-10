using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    COMP397Labs inputs;

    [SerializeField] Button turnCameraLeft;
    [SerializeField] Button turnCameraRight;
    [SerializeField] int index = 0;
    [SerializeField] CinemachineVirtualCamera currentCamera;
    [SerializeField] List<CinemachineVirtualCamera> virtualCameras = new List<CinemachineVirtualCamera>();
    [SerializeField] PlayerController playerController;
    [SerializeField] List<float> rotations = new List<float>() { 0, 90, 180, 270 };
    //Vector3 targetRotation = Vector3.forward;

    float timer = 0f;
    float rotationTime = 0.2f;
    private void Awake()
    {
        InitCameraPriorities();

        inputs = new COMP397Labs();

        inputs.Player.Camera.performed += context => MoveCamera(context.ReadValue<float>());

        turnCameraLeft.onClick.AddListener(() => MoveCamera(-1));
        turnCameraRight.onClick.AddListener(() => MoveCamera(1));
    }

    private void Update()
    {
        //timer += Time.deltaTime;

        //if(timer < rotationTime)
        //{
        //    var r = Mathf.Lerp(playerController.transform.rotation.y, rotations[index], timer / rotationTime);
        //    playerController.transform.rotation = Quaternion.Euler(0, r, 0f);
        //}

        //playerController.transform.rotation = Quaternion.RotateTowards(playerController.transform.rotation, Quaternion.LookRotation(direction), 100 * Time.deltaTime);
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

        if (index >= virtualCameras.Count)
            index = 0;
        else if (index < 0)
            index = virtualCameras.Count-1;
        
        currentCamera.Priority = 0;
        currentCamera = virtualCameras[index];
        currentCamera.Priority = 10;

        //Instead of change camera, rotate the player
        //var targetRotation = new Vector3(0, rotations[index], 0);

        //playerController.transform.rotation = Quaternion.Euler(0, rotations[index], 0f);
        //timer = 0;

    }

}
