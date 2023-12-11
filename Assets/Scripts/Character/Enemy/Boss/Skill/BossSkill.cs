using GlobalEnums;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossSkill : ActionNode
{
    protected BossAnimationController animationController;
    protected GameObject defaultWeapon;

    public BossSkill(BossBehaviorTree bossBehaviourTree) : base(bossBehaviourTree)
    {
        animationController = bossBehaviourTree.AnimationController;
        defaultWeapon = bossBehaviourTree.DefaultWeapon;
        btDict = bossBehaviourTree.BTDict;
    }

    protected void UseSkill()
    {
        bossBehaviourTree.LookRightAway();

        animationController.PlayAnimation(animationController.AnimationData.AttackSubStateParameterHash, true);
        animationController.PlayAnimation(animationController.AnimationData.SkillSubParameterHash, true);
        animationController.PlayAnimation(animationController.AnimationData.PhaseParameterHash, bossBehaviourTree.CurrentPhase);
        btDict[BTValues.CurrentAction] = CurrentAction.UsingSkill;
    }
    protected void OnAnimationEnded()
    {
        animationController.PlayAnimation(animationController.AnimationData.AttackSubStateParameterHash, false);
        animationController.PlayAnimation(animationController.AnimationData.SkillSubParameterHash, false);
        btDict[BTValues.CurrentAction] = CurrentAction.Patrol;
        btDict[BTValues.IsAttacking] = false;
        defaultWeapon.SetActive(true);
    }

    protected virtual void OnChargedCoolTime()
    {
        btDict[BTValues.CurrentSkillElapsedTime] = 0f;
        btDict[BTValues.IsAttacking] = true;
    }

    protected abstract void Init();
}