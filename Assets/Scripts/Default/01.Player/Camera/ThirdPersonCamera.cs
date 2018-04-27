using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

    [SerializeField]
    private Transform camTransform;

    [SerializeField]
    private Transform pivot;

    [SerializeField]
    private Transform character;

    [SerializeField]
    private Transform mTransform;

    [SerializeField]
    CharacterStatus characterStatus;

    [SerializeField]
    CameraConfig cameraConfig;

    [SerializeField]
    private bool leftPivot;

    [SerializeField]
    private float delta;

    [SerializeField]
    private float mouseX;

    [SerializeField]
    private float mouseY;

    [SerializeField]
    private float smoothX;

    [SerializeField]
    private float smoothY;

    [SerializeField]
    private float smoothXAxisVelocity;

    [SerializeField]
    private float smoothYAxisVelocity;

    [SerializeField]
    private float lookAngle;

    [SerializeField]
    private float tiltingAngle;

    void Update()
    {
        FixedTick();
    }

    void FixedTick()
    {
        delta = Time.deltaTime;

        HandlePosition();
        HandleRotation();

        Vector3 targetPosition = Vector3.Lerp(mTransform.position, character.position, 1);
        mTransform.position = targetPosition;
    }

    void HandlePosition()
    {
        float targetX = cameraConfig.normalX;
        float targetY = cameraConfig.normalY;
        float targetZ = cameraConfig.normalZ;

        if(characterStatus.isAiming)
        {
            targetX = cameraConfig.aimX;
            targetZ = cameraConfig.aimZ;
        }

        if(leftPivot)
        {
            targetX = -targetX;
        }

        Vector3 newPivotPosition = pivot.localPosition;
        newPivotPosition.x = targetX;
        newPivotPosition.y = targetY;

        Vector3 newCameraPosition = camTransform.localPosition;
        newCameraPosition.z = targetZ;

        float t = delta * cameraConfig.pivotSpeed;
        pivot.localPosition = Vector3.Lerp(pivot.localPosition, newPivotPosition, t);
        camTransform.localPosition = Vector3.Lerp(camTransform.localPosition, newCameraPosition, t);
    }

    void HandleRotation()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        if(cameraConfig.turnSmooth > 0)
        {
            smoothX = Mathf.SmoothDamp(smoothX, mouseX, ref smoothXAxisVelocity, cameraConfig.turnSmooth);
            smoothY = Mathf.SmoothDamp(smoothY, mouseY, ref smoothYAxisVelocity, cameraConfig.turnSmooth);
        }
        else
        {
            smoothX = mouseX;
            smoothY = mouseY;
        }

        lookAngle += smoothX * cameraConfig.yAxisRotationSpeed;

        Quaternion targetRotation = Quaternion.Euler(0, lookAngle, 0);
        mTransform.rotation = targetRotation;

        tiltingAngle -= smoothY * cameraConfig.xAxisRotationSpeed;
        tiltingAngle = Mathf.Clamp(tiltingAngle, cameraConfig.minAngle, cameraConfig.maxAngle);
        pivot.localRotation = Quaternion.Euler(tiltingAngle, 0, 0);
    }
}
