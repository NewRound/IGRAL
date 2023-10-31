using UnityEngine;

public class RangedEntityData : ScriptableObject
{
    [field: SerializeField] public float ProjectileSpeed { get; set; }
    [field: SerializeField] public float ProjectileDuration { get; set; }
}
