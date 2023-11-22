using GlobalEnums;
using System.Collections.Generic;
using UnityEngine;

public class CheckSkillCoolTimeNode : Node
{
    private BossBehaviourTree _bossBehaviourTree;
    private Dictionary<BTValues, object> _btDict = new Dictionary<BTValues, object>();

    public CheckSkillCoolTimeNode(BossBehaviourTree bossBehaviourTree)
    {
        _bossBehaviourTree = bossBehaviourTree;
        _btDict = _bossBehaviourTree.BTDict;
    }

    public override NodeState Evaluate()
    {
        if (!_btDict.ContainsKey(BTValues.CurrentPhaseSkillCoolTime) || !_btDict.ContainsKey(BTValues.CurrentSkillElapsedTime))
        {
            state = NodeState.Failure;
            return state;
        }

        if ((float)_btDict[BTValues.CurrentSkillElapsedTime] >= (float)_btDict[BTValues.CurrentPhaseSkillCoolTime])
        {
            _btDict[BTValues.CurrentSkillElapsedTime] = 0f;

            state = NodeState.Success;
            return state;
        }

        state = NodeState.Failure;
        return state;
    }
}