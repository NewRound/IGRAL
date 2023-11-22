using GlobalEnums;
using System.Collections.Generic;
using UnityEngine;

public class RunningCoolTimeNode : Node
{
    private BossBehaviourTree _bossBehaviourTree;
    private PhaseSO _phaseSO;
    private float _currentElapsedTime;
    private Dictionary<BTValues, object> _btDict;

    public RunningCoolTimeNode(BossBehaviourTree bossBehaviourTree)
    {
        _bossBehaviourTree = bossBehaviourTree;
        _phaseSO = _bossBehaviourTree.PhaseSO;
        _btDict = _bossBehaviourTree.BTDict;
    }

    public override NodeState Evaluate()
    {
        float skillCoolTime = _phaseSO.PhaseInfo[_bossBehaviourTree.CurrentPhase - 1].SkillCoolTime;

        _btDict[BTValues.CurrentPhaseSkillCoolTime] = skillCoolTime;

        if ((bool)_btDict[BTValues.WasSkillUsed])
        {
            _btDict[BTValues.WasSkillUsed] = false;
            _currentElapsedTime = 0;
        }

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