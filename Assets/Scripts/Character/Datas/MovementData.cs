using System;
using UnityEngine;

[Serializable]
public class MovementData
{
    [field : SerializeField] public Transform ModelTrans { get; private set; }
    [field: SerializeField] public float AcceleratingTime { get; private set; } = 1f;
    [field: SerializeField] public float RotationSpeed { get; private set; } = 10f;
}