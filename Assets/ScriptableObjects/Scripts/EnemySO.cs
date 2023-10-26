using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "SO/CharacterData/EnemyData")]
public class EnemySO : EntitySO
{
    [field: SerializeField] public float PreAttackDelay { get; private set; }
    [field: SerializeField] public float TraceRange { get; private set; }
    [field: SerializeField] public bool IsFlying { get; private set; }
}
