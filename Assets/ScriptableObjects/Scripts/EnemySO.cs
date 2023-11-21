using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "SO/EntityData/EnemyData")]
public class EnemySO : EntitySO
{
    [field: Header("Enemy")]
    [field: SerializeField] public int EnemyID { get; set; }
    [field: SerializeField] public float PreAttackDelay { get; set; }
    [field: SerializeField] public bool IsFlying { get; set; }
    [field: SerializeField] public float AttackDistance { get; set; }

    [field: SerializeField] public float Armor { get; set; }
    [field: SerializeField] public float MaxArmor { get; set; }

    [field: Header("Ranged")]
    [field: SerializeField] public float ProjectileSpeed { get; set; }
    [field: SerializeField] public float ProjectileDuration { get; set; }
}
