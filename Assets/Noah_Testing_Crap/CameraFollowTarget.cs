using System;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class CameraFollowTarget : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Vector3 directionFromTargetToCamera;
    [SerializeField] private float distanceBetweenTargetAndCamera;
    [SerializeField] private float smoothedOutLerp;
    private Camera sceneMainCamera;

    private Vector3 targetToCamera;

    private void OnEnable()
    {
        sceneMainCamera = Camera.main;
    }

    private void OnDisable()
    {
        
    }

    private void Awake()
    {
        directionFromTargetToCamera.Normalize();
        targetToCamera = distanceBetweenTargetAndCamera * directionFromTargetToCamera;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Do once a rotation for camera to look at camera without doing "LookAt"
        
        sceneMainCamera.transform.localRotation = Quaternion.Euler(distanceBetweenTargetAndCamera*Mathf.Acos(directionFromTargetToCamera.z), 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredCameraPosition = targetTransform.position + targetToCamera;
        Vector3 smoothedCameraPosition = Vector3.Slerp(sceneMainCamera.transform.position, desiredCameraPosition, smoothedOutLerp);
        Vector3 toTranslate = smoothedCameraPosition - sceneMainCamera.transform.position;
        sceneMainCamera.transform.Translate(toTranslate);
        //sceneMainCamera.transform.LookAt(targetTransform);
        
    }
}
