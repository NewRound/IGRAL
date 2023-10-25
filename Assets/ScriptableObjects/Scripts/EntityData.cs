using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityData : HealthData
{
    [field: SerializeField] public float Attack { get; private set; }
    [field: SerializeField] public float AttackDelay { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public float CriticalProbability { get; private set; }
    [field: SerializeField] public float CriticalMod { get; private set; }
    [field: SerializeField] public float SpeedMin { get; private set; }
    [field: SerializeField] public float SpeedMax { get; private set; }
    [field: SerializeField] public float KnockbackPower { get; private set; }
    [field: SerializeField] public float KnockbackTime { get; private set; }
    [field: SerializeField] public bool IsFlying { get; private set; }
    [field: SerializeField] public bool IsRanged { get; private set; }

}
