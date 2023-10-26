using System;
using UnityEngine;

[Serializable]
public class Movement
{
    [Header("Acceleration")]
    public float acceleratingTime = 1f;

    [Header("Rotation")]
    public float rotationSpeed = 10f;
    public float minAbsAngle = 90f;
    public float maxAbsAngle = 270f;
}