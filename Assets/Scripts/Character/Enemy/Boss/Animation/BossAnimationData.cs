using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BossAnimationData : CharacterAnimationsData
{
    [SerializeField] private string skillSubParameter = "@Skill";
    [SerializeField] private string phaseParameter = "Phase";
    [SerializeField] private string meleeAttackParameter = "MeleeAttack";

    public int SkillSubParameterHash { get; private set; }
    public int PhaseParameterHash { get; private set; }
    public int MeleeAttackParameterHash { get; private set; }

    public override void Init()
    {
        base.Init();
        SkillSubParameterHash = Animator.StringToHash(skillSubParameter);
        PhaseParameterHash = Animator.StringToHash(phaseParameter);
        MeleeAttackParameterHash = Animator.StringToHash(meleeAttackParameter);
    }
}
