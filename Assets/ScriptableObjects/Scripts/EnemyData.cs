using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "CreateData/EnemyData")]
public class EnemyData : EntityData
{
    [field: SerializeField] public float PreAttackDelay { get; private set; }
    [field: SerializeField] public float TraceRange { get; private set; }
    [field: SerializeField] public bool IsFlying { get; private set; }
}
