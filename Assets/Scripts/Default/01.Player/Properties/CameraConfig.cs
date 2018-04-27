using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Config")]
public class CameraConfig : ScriptableObject
{
    public float turnSmooth;
    public float pivotSpeed;
    public float yAxisRotationSpeed;
    public float xAxisRotationSpeed;
    public float minAngle;
    public float maxAngle;
    public float normalZ;
    public float normalX;
    public float normalY;
    public float aimX;
    public float aimZ;
}
