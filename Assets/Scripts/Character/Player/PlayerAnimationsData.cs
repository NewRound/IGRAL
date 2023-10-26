using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAnimationsData
{
    [SerializeField] private string moveParameter = "Move";
    [SerializeField] private string jumpParameter = "Jump";
    [SerializeField] private string slideParameter = "Slide";
    [SerializeField] private string attackParameter = "Attack";
    [SerializeField] private string speedRatioParameter = "SpeedRatio";

    public int MoveParameterHash { get; private set; }
    public int JumpParameterHash { get; private set; }
    public int SlideParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }
    public int SpeedRatioParameterHash { get; private set; }

    public void Init()
    {
        MoveParameterHash = Animator.StringToHash(moveParameter);
        JumpParameterHash = Animator.StringToHash(jumpParameter);
        SlideParameterHash = Animator.StringToHash(slideParameter);
        AttackParameterHash = Animator.StringToHash(attackParameter);
        SpeedRatioParameterHash = Animator.StringToHash(speedRatioParameter);
    }
}

