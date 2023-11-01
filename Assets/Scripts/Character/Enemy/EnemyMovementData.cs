using System;
using UnityEngine;

[Serializable]
public class EnemyMovementData : MovementData
{
    [field: SerializeField] public float PatrolAnimationRatio { get; private set; } = 0.3f;
}