using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "SO/EntityData/PlayerData")]
public class PlayerSO : EntitySO
{
    [field: SerializeField] public float JumpForce { get; private set; }
    [field: SerializeField] public float Combo { get; private set; }
    [field: SerializeField] public float MaxJumpCount { get; private set; }
    [field: SerializeField] public float KcalPerAttack { get; private set; }
    [field: SerializeField] public float MaxKcal { get; private set; }
    [field: SerializeField] public float WallSlidingTime { get; private set; }
    [field: SerializeField] public float WallSlidingSpeed { get; private set; }
}
