using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BossAnimationData : CharacterAnimationsData
{
    [SerializeField] private string skillParameter = "Skill";
    [SerializeField] private string PhaseParameter = "Phase";

    public int SkillParameterHash { get; private set; }
    public int PhaseParameterHash { get; private set; }

    public override void Init()
    {
        base.Init();
        SkillParameterHash = Animator.StringToHash(skillParameter);
        PhaseParameterHash = Animator.StringToHash(PhaseParameter);
    }
}
