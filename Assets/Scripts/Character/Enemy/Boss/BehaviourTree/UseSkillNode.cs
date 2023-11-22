using GlobalEnums;
using System.Collections.Generic;
using UnityEngine;

public class UseSkillNode : Node
{
    private BossBehaviourTree _bossBehaviourTree;
    private BossAnimationController _animationController;
    private PhaseSO _phaseSO;
    private Dictionary<BTValues, object> _btDict = new Dictionary<BTValues, object>();

    public UseSkillNode(BossBehaviourTree bossBehaviourTree)
    {
        _bossBehaviourTree = bossBehaviourTree;
        _animationController = _bossBehaviourTree.AnimationController;
        _phaseSO = _bossBehaviourTree.PhaseSO;
        _btDict = _bossBehaviourTree.BTDict;
    }

    public override NodeState Evaluate()
    {
        float normalizedTime = AnimationUtil.GetNormalizeTime(_animationController.Animator, AnimTag.Action, (int)AnimatorLayer.UpperLayer);
        if (normalizedTime > 1f)
        {
            _animationController.PlayAnimation(_animationController.AnimationData.AttackSubStateParameterHash, false);
            _animationController.PlayAnimation(_animationController.AnimationData.SkillParameterHash, false);
            _btDict[BTValues.IsAnyActionPlaying] = false;
            _btDict[BTValues.WasSkillUsed] = false;
        }

        _animationController.PlayAnimation(_animationController.AnimationData.AttackSubStateParameterHash, true);
        _animationController.PlayAnimation(_animationController.AnimationData.SkillParameterHash, true);
        _animationController.PlayAnimation(_animationController.AnimationData.PhaseParameterHash, _bossBehaviourTree.CurrentPhase);
        _btDict[BTValues.IsAnyActionPlaying] = true;
        _btDict[BTValues.WasSkillUsed] = true;

        //_phaseSO.PhaseInfo[_bossBehaviourTree.CurrentPhase - 1].weaponPrefab;
        state = NodeState.Success; 
        return state;
    }
}