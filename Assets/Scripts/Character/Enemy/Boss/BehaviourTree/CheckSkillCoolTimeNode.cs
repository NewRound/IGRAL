using GlobalEnums;
using System.Collections.Generic;
using UnityEngine;

public class CheckSkillCoolTimeNode : Node
{
    private BossBehaviourTree _bossBehaviourTree;
    private Dictionary<BTValues, object> _btDict = new Dictionary<BTValues, object>();
    private BossAnimationController _animationController;

    public CheckSkillCoolTimeNode(BossBehaviourTree bossBehaviourTree)
    {
        _bossBehaviourTree = bossBehaviourTree;
        _animationController = _bossBehaviourTree.AnimationController;
        _btDict = _bossBehaviourTree.BTDict;
    }

    public override NodeState Evaluate()
    {
        return GetCoolTimeState();
    }

    private NodeState GetCoolTimeState()
    {
        if ((float)_btDict[BTValues.CurrentSkillElapsedTime] >= (float)_btDict[BTValues.CurrentPhaseSkillCoolTime] &&
            !(bool)_btDict[BTValues.WasSkillUsed])
        {
            _btDict[BTValues.CurrentSkillElapsedTime] = 0f;

            state = NodeState.Success;
            return state;
        }

        state = NodeState.Failure;
        return state;
    }
}