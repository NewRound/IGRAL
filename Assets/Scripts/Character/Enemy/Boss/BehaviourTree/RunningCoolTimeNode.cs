using GlobalEnums;
using System.Collections.Generic;
using UnityEngine;

public class RunningCoolTimeNode : Node
{
    private BossBehaviourTree _bossBehaviourTree;
    private float _currentElapsedTime;
    private Dictionary<BTValues, object> _btDict;

    public RunningCoolTimeNode(BossBehaviourTree bossBehaviourTree)
    {
        _bossBehaviourTree = bossBehaviourTree;
        _btDict = _bossBehaviourTree.BTDict;
    }

    public override NodeState Evaluate()
    {
        float skillCoolTime = _bossBehaviourTree.PhaseInfoArr[_bossBehaviourTree.CurrentPhase - 1].SkillCoolTime;

        _btDict[BTValues.CurrentPhaseSkillCoolTime] = skillCoolTime;

        if ((bool)_btDict[BTValues.WasSkillUsed])
            _currentElapsedTime = 0f;

        if (_currentElapsedTime < skillCoolTime)
        {
            _currentElapsedTime += Time.deltaTime;
            _currentElapsedTime = _currentElapsedTime > skillCoolTime ? skillCoolTime : _currentElapsedTime;

            _btDict[BTValues.CurrentSkillElapsedTime] = _currentElapsedTime;
            // TODO : BossUI랑 연결
        }

        state = NodeState.Success;
        return state;
    }
}