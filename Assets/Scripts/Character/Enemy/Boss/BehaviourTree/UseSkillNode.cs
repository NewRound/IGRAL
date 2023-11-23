using GlobalEnums;
using System.Collections.Generic;
using UnityEngine;

public class UseSkillNode : Node
{
    private BossBehaviourTree _bossBehaviourTree;
    private BossAnimationController _animationController;
    private Dictionary<BTValues, object> _btDict = new Dictionary<BTValues, object>();

    private Boss3Phase1 _phase1;

    public UseSkillNode(BossBehaviourTree bossBehaviourTree)
    {
        _bossBehaviourTree = bossBehaviourTree;
        _animationController = _bossBehaviourTree.AnimationController;
        _btDict = _bossBehaviourTree.BTDict;
        _phase1 = new Boss3Phase1(_bossBehaviourTree.DefaultWeapon, _bossBehaviourTree.BulletSpawnTrans, _animationController);
    }

    public override NodeState Evaluate()
    {
        SetAttackEvent();

        _bossBehaviourTree.LookRightAway();

        _animationController.PlayAnimation(_animationController.AnimationData.AttackSubStateParameterHash, true);
        _animationController.PlayAnimation(_animationController.AnimationData.SkillSubParameterHash, true);
        _animationController.PlayAnimation(_animationController.AnimationData.PhaseParameterHash, _bossBehaviourTree.CurrentPhase);
        _btDict[BTValues.IsAnyActionPlaying] = true;
        _btDict[BTValues.WasSkillUsed] = true;

        state = NodeState.Success;
        return state;
    }

    private void SetAttackEvent()
    {
        _animationController.AttackAction += _phase1.UseSkill;
        _animationController.PreSkillAction += _phase1.ChangeToNewWeapon;
        _animationController.PostSkillAction += _phase1.ChangeToDefaultWeapon;
    }
}