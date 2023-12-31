﻿using System;
using UnityEngine;

[Serializable]
public class EnemyMovementData : MovementData
{
    [field: SerializeField] public float PatrolAnimationRatio { get; private set; } = 0.3f;
    [field: SerializeField] public int WaitPatrolTime { get; private set; } = 2;
}