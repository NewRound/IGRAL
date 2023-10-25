using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class HealthSO : ScriptableObject
{
    [field: SerializeField] public float Health { get; private set; }
    [field: SerializeField] public float HealthRegen { get; private set; }
    [field: SerializeField] public float Defense { get; private set; }
    [field: SerializeField] public float EvasionProbability { get; private set; }
    [field: SerializeField] public bool IsInvincible { get; private set; }
    [field: SerializeField] public float InvincibleTime { get; private set; }
}
