using System;
using UnityEngine;

public class BossAnimationController : AnimationController
{
    [field: SerializeField] public BossAnimationData AnimationData { get; private set; }

    public event Action PreSkillAction;
    public event Action PostSkillAction;

    public override void Init()
    {
        base.Init();
        AnimationData.Init();
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
