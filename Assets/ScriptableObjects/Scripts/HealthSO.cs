using UnityEngine;


public abstract class HealthSO : ScriptableObject
{
    [field: SerializeField] public float Health { get; set; }
    [field: SerializeField] public float MaxHealth { get; set; }
    [field: SerializeField] public float HealthRegen { get; set; }
    [field: SerializeField] public float Defense { get; set; }
    [field: SerializeField] public float EvasionProbability { get; set; }
    [field: SerializeField] public bool IsInvincible { get; set; }
    [field: SerializeField] public float InvincibleTime { get; set; }
}
