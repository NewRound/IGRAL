using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimTag
{
    Attack,
    Action
}

public class PlayerAnimationController : AnimationController
{
    [field: SerializeField] public PlayeranimationsData AnimationData { get; private set; }

    public int AttackCombo { get; private set; }

    public override void Init()
    {
        base.Init();
        AnimationData.Init();
    }

    public void ResetCombo()
    {
        AttackCombo = 0;
    }

    public void IncreaseCombo()
    {
        AttackCombo++;
    }
}
