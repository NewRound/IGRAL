using System;
using System.Collections;
using UnityEngine;

public abstract class AnimationController : MonoBehaviour
{
    protected Animator animator;

    [field: SerializeField] public float animationNormalizeEndedTime = 0.9f;

    public event Action AttackAction;

    private void AttackEvent()
    {
        AttackAction?.Invoke();
    }

    public virtual void Init()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnimation(int animationParameterHash, bool isPlaying)
    {
        animator.SetBool(animationParameterHash, isPlaying);
    }

    public void PlayAnimation(int animationParameterHash)
    {
        animator.SetTrigger(animationParameterHash);
    }

    public void PlayAnimation(int animationParameterHash, int integerValue)
    {
        animator.SetInteger(animationParameterHash, integerValue);
    }

    public void PlayAnimation(int animationParameterHash, float floatValue)
    {
        animator.SetFloat(animationParameterHash, floatValue);
    }

}
