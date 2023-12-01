using System;
using UnityEngine;

public class BossAnimationController : AnimationController
{
    [field: SerializeField] public BossAnimationData AnimationData { get; private set; }

    public event Action PreSkillAction;
    public event Action PostSkillAction;
    public event Action MeleeAttackAction;

    public override void Init()
    {
        base.Init();
        AnimationData.Init();
    }

    private void MeleeAttackEvent()
    {
        MeleeAttackAction?.Invoke();
    }

    private void PreSkillEvent()
    {
        PreSkillAction?.Invoke();
    }

    private void PostSkillEvent()
    {
        PostSkillAction?.Invoke();
    }
}
