using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "SO/EntityData/PlayerData")]
public class PlayerSO : EntitySO
{
    [field : Header("Player")]
    [field: SerializeField] public float JumpingForce { get; private set; }
    [field: SerializeField] public int JumpingCountMax { get; private set; }
    [field: SerializeField] public float RollingForce { get; private set; }
    [field: SerializeField] public float RollingCoolTime { get; private set; }
    [field: SerializeField] public float KcalPerAttack { get; private set; }
    [field: SerializeField] public float MaxKcal { get; private set; }
    [field: SerializeField] public float WallSlidingTime { get; private set; }
    [field: SerializeField] public float WallSlidingSpeed { get; private set; }
}
