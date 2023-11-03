using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "SO/EntityData")]
public class EntitySO : HealthSO
{
    [field: Header("Entity")]
    [field: SerializeField] public float Attack { get; set; }
    [field: SerializeField] public float AttackDelay { get; set; }
    [field: SerializeField] public float AttackRange { get; set; }
    [field: SerializeField] public float CriticalProbability { get; set; }
    [field: SerializeField] public float CriticalMod { get; set; }
    [field: SerializeField] public float SpeedMin { get; set; }
    [field: SerializeField] public float SpeedMax { get; set; }
    [field: SerializeField] public float KnockbackPower { get; set; }
    [field: SerializeField] public bool IsRanged { get; set; }

}
