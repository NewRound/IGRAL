using System;
using System.Collections;
using UnityEngine;

public abstract class AnimationController : MonoBehaviour
{
    public Animator Animator { get; private set; }

    [field: SerializeField] public float animationNormalizeEndedTime = 0.9f;

    public event Action AttackAction;

    private void AttackEvent()
    {
        AttackAction?.Invoke();
    }

    public virtual void Init()
    {
        Animator = GetComponent<Animator>();
    }

    public void PlayAnimation(int animationParameterHash, bool isPlaying)
    {
        Animator.SetBool(animationParameterHash, isPlaying);
    }

    public void PlayAnimation(int animationParameterHash)
    {
        Animator.SetTrigger(animationParameterHash);
    }

    public void PlayAnimation(int animationParameterHash, int integerValue)
    {
        Animator.SetInteger(animationParameterHash, integerValue);
    }

    public void PlayAnimation(int animationParameterHash, float floatValue)
    {
        Animator.SetFloat(animationParameterHash, floatValue);
    }

}
