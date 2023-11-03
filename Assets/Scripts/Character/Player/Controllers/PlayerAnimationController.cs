using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    MeleeAttack1 = 1,
    MeleeAttack2,
    MeleeAttack3,
    MeleeAttack4,
}

public class PlayerAnimationController : AnimationController
{
    [field: SerializeField] public PlayeranimationsData AnimationData { get; private set; }

    public bool IsAttackInputted { get; private set; }

    public int AttackCombo { get; private set; }

    public AttackType AttackType { get; private set; }

    private int _attackTypeLength;

    public override void Init()
    {
        base.Init();
        AnimationData.Init();
        AttackType = AttackType.MeleeAttack1;
        _attackTypeLength = Enum.GetValues(typeof(AttackType)).Length;
    }

    public void ReStartIfAnimationIsPlaying(int animationParameterHash, int layerIndex = 0)
    {
        if (animator.GetCurrentAnimatorStateInfo(layerIndex).shortNameHash.Equals(animationParameterHash))
        {
            animator.Play(animationParameterHash, layerIndex, 0f);
        }
    }

    public bool CheckCurrentAnimationEnded(int animationHash, int layerIndex = 0)
    {
        var stateInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);
        if (stateInfo.shortNameHash.Equals(animationHash))
        {
            if (stateInfo.normalizedTime >= animationNormalizeEndedTime)
                return true;
        }

        return false;
    }

    
    
    public bool CheckCurrentClipEqual(AttackType attackType, int layerIndex = 0)
    {
        var clipInfo = animator.GetCurrentAnimatorClipInfo(layerIndex);
        Debug.Log($"clipInfo[0].clip.name : {clipInfo[0].clip.name}\nattackType : {attackType}");

        if (clipInfo[0].clip.name.Equals(attackType.ToString()))
            return true;
        else
            return false;
    }

    public void OnAttackInputted()
    {
        SetAttackInputted(true);
    }

    public void SetAttackInputted(bool isInputted)
    {
        IsAttackInputted = isInputted;
    }

    public void IncreaseAttackCombo()
    {
        AttackCombo = AttackCombo >= _attackTypeLength ? AttackCombo : ++AttackCombo;
    }

    public void ResetCombo()
    {
        AttackCombo = 0;
    }

    public void SetNextAttackType()
    {
        AttackType = (AttackType)AttackCombo;
    }


    public void ReSetAttackType()
    {
        AttackType = AttackType.MeleeAttack1;
    }
}
