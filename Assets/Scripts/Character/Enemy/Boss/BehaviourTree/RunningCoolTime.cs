using GlobalEnums;
using System.Collections.Generic;
using UnityEngine;

public class RunningCoolTime : BossNode
{
    private float _currentElapsedTime;
    public RunningCoolTime(BossBehaviourTree bossBehaviourTree) : base(bossBehaviourTree)
    {
    }

    public override NodeState Evaluate()
    {
        float skillCoolTime = bossBehaviourTree.PhaseInfoArr[bossBehaviourTree.CurrentPhase - 1].SkillCoolTime;

        btDict[BTValues.CurrentPhaseSkillCoolTime] = skillCoolTime;

        if ((CurrentAction)btDict[BTValues.CurrentAction] == CurrentAction.Attack || 
            (CurrentAction)btDict[BTValues.CurrentAction] == CurrentAction.UsingSkill)
            _currentElapsedTime = 0f;

        if (_currentElapsedTime < skillCoolTime)
        {
            _currentElapsedTime += Time.deltaTime;
            _currentElapsedTime = _currentElapsedTime > skillCoolTime ? skillCoolTime : _currentElapsedTime;

            btDict[BTValues.CurrentSkillElapsedTime] = _currentElapsedTime;
            // TODO : BossUI랑 연결
        }

        state = NodeState.Success;
        return state;
    }
}