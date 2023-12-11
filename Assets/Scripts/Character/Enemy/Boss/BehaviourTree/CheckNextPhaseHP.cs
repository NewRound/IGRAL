using GlobalEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckNextPhaseHP : BossNode
{
    private int _totalPhaseCount;

    public CheckNextPhaseHP(BossBehaviorTree bossBehaviourTree) : base(bossBehaviourTree)
    {
        _totalPhaseCount = this.bossBehaviourTree.PhaseInfoArr.Length;
    }

    public override NodeState Evaluate()
    {
        float nextPhaseHealth = bossBehaviourTree.StatHandler.Data.MaxHealth * (_totalPhaseCount - bossBehaviourTree.CurrentPhase) / _totalPhaseCount ;

        if (nextPhaseHealth > bossBehaviourTree.StatHandler.Data.Health)
        {
            state = NodeState.Success;
            return state;
        }

        state = NodeState.Failure;
        return state;
    }
}
