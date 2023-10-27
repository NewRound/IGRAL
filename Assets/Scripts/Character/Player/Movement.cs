using System;
using UnityEngine;

[Serializable]
public class Movement
{
    [field: SerializeField] public float AcceleratingTime { get; private set; } = 1f;
    [field: SerializeField] public float RotationSpeed { get; private set; } = 10f;
    [field: SerializeField] public float MinAbsAngle { get; private set; } = 90f;
    [field: SerializeField] public float MaxAbsAngle { get; private set; } = 270f;
}