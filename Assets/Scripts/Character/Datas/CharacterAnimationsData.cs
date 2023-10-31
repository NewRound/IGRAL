using System;
using UnityEngine;

[Serializable]
public class CharacterAnimationsData
{
    [SerializeField] private string moveParameter = "@Move";
    [SerializeField] private string airParameter = "@Air";
    [SerializeField] private string attackParameter = "@Attack";
    [SerializeField] private string jumpParameter = "Jump";
    [SerializeField] private string fallParameter = "Fall";
    [SerializeField] private string RollParameter = "Roll";
    [SerializeField] private string speedRatioParameter = "SpeedRatio";

    public int MoveParameterHash { get; private set; }
    public int AirParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }
    public int JumpParameterHash { get; private set; }
    public int FallParameterHash { get; private set; }
    public int RollParameterHash { get; private set; }
    public int SpeedRatioParameterHash { get; private set; }

    public void Init()
    {
        MoveParameterHash = Animator.StringToHash(moveParameter);
        AirParameterHash = Animator.StringToHash(airParameter);
        AttackParameterHash = Animator.StringToHash(attackParameter);
        JumpParameterHash = Animator.StringToHash(jumpParameter);
        FallParameterHash = Animator.StringToHash(fallParameter);
        RollParameterHash = Animator.StringToHash(RollParameter);
        SpeedRatioParameterHash = Animator.StringToHash(speedRatioParameter);
    }
}

