using System;
using UnityEngine;

[Serializable]
public class GroundData
{
    [field: SerializeField] public LayerMask GroundLayer { get; private set; }
    [field: SerializeField] public float GroundYOffset { get; private set; } = 0.2f;
    [field: SerializeField] public float GroundRadiusMod { get; private set; } = 1.5f;
}
