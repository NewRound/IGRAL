using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "SO/EntityData/PlayerData")]
public class PlayerSO : EntitySO
{
    [field : Header("Player")]
    [field: SerializeField] public float JumpingForce { get; set; }
    [field: SerializeField] public int JumpingCountMax { get; set; }
    [field: SerializeField] public float RollingForce { get; set; }
    [field: SerializeField] public float RollingCoolTime { get; set; }
    [field: SerializeField] public float KcalPerAttack { get; set; }
    [field: SerializeField] public float Kcal { get; set; }
    [field: SerializeField] public float MaxKcal { get; set; }
    [field: SerializeField] public float WallSlidingTime { get; set; }
    [field: SerializeField] public float KnockbackTime { get; set; }
    [field: SerializeField] public float WallSlidingSpeed { get; set; }
}
