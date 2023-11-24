using GlobalEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckNextPhaseHP : BossNode
{
    private int _totalPhaseCount;
    private int _currentPhase = 1;

    public CheckNextPhaseHP(BossBehaviourTree bossBehaviourTree) : base(bossBehaviourTree)
    {
        _totalPhaseCount = this.bossBehaviourTree.PhaseInfoArr.Length;
    }

    public override NodeState Evaluate()
    {
        float nextPhaseHealth = bossBehaviourTree.StatHandler.Data.MaxHealth * (_totalPhaseCount - _currentPhase) / _totalPhaseCount ;

        if (nextPhaseHealth > bossBehaviourTree.StatHandler.Data.Health)
        {
            state = NodeState.Success;
            return state;
        }

        state = NodeState.Failure;
        return state;
    }
}
