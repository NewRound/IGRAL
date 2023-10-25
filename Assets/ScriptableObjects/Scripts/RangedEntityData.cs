using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEntityData : ScriptableObject
{
    [field: SerializeField] public float ProjectileSpeed { get; private set; }
    [field: SerializeField] public float ProjectileDuration { get; private set; }
}
