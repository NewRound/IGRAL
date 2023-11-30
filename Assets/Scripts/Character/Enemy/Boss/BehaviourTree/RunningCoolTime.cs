using GlobalEnums;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RunningCoolTime : BossNode
{
    public RunningCoolTime(BossBehaviourTree bossBehaviourTree) : base(bossBehaviourTree)
    {
    }

    public override NodeState Evaluate()
    {
        float skillCoolTime = bossBehaviourTree.PhaseInfoArr[bossBehaviourTree.CurrentPhase - 1].SkillCoolTime;

        btDict[BTValues.CurrentPhaseSkillCoolTime] = skillCoolTime;

        float currentElapsedTime = (float)btDict[BTValues.CurrentSkillElapsedTime];

        if (currentElapsedTime <= skillCoolTime)
        {
            currentElapsedTime += Time.deltaTime;
            currentElapsedTime = currentElapsedTime > skillCoolTime ? skillCoolTime : currentElapsedTime;

            btDict[BTValues.CurrentSkillElapsedTime] = currentElapsedTime;
            // TODO : BossUI랑 연결
        }

        state = NodeState.Success;
        return state;
    }

    
}