using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimTag
{
    Attack,
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
    
    public float GetNormalizeTime(int animationHash, int layerIndex = 0)
    {
        var stateInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);
        return stateInfo.normalizedTime;
    }

    public float GetNormalizeTime(AnimTag animTag, int layerIndex = 0)
    {
        string tag = animTag.ToString();
        var currentStateInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);
        var nextStateInfo = animator.GetNextAnimatorStateInfo(layerIndex);

        if (animator.IsInTransition(layerIndex))
        {
            if (nextStateInfo.IsTag(tag))
                return nextStateInfo.normalizedTime;
        }
        else
        {
            if (currentStateInfo.IsTag(tag))
                return currentStateInfo.normalizedTime;
        }

        return 0;
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
