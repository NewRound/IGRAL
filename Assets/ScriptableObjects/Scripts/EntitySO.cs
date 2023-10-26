using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "SO/EntityData")]
public class EntitySO : HealthSO
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
    [field: SerializeField] public bool IsRanged { get; private set; }

}
