using GlobalEnums;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossSkill : ActionNode
{
    protected BossAnimationController animationController;
    public BossSkill(BossBehaviourTree bossBehaviourTree) : base(bossBehaviourTree)
    {
        animationController = bossBehaviourTree.AnimationController;
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

   
}