using System;
using UnityEngine;

[Serializable]
public class PlayeranimationsData : CharacterAnimationsData
{
    [SerializeField] private string airParameter = "@Air";
    [SerializeField] private string jumpParameter = "Jump";
    [SerializeField] private string fallParameter = "Fall";
    [SerializeField] private string rollParameter = "Roll";
    [SerializeField] private string attackComboParameter = "AttackCombo";

    public int AirSubStateParameterHash { get; private set; }
    public int JumpParameterHash { get; private set; }
    public int FallParameterHash { get; private set; }
    public int RollParameterHash { get; private set; }
    public int AttackComboHash { get; private set; }

    public override void Init()
    {
        base.Init();
        AirSubStateParameterHash = Animator.StringToHash(airParameter);
        JumpParameterHash = Animator.StringToHash(jumpParameter);
        FallParameterHash = Animator.StringToHash(fallParameter);
        RollParameterHash = Animator.StringToHash(rollParameter);
        AttackComboHash = Animator.StringToHash(attackComboParameter);
    }

}