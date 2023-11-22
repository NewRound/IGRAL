using GlobalEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHpNode : Node
{
    private BossBehaviourTree _bossBehaviourTree;
    private int _totalPhase;
    private int _currentPhase = 1;

    // PhaseSO로 바꿔야할 듯 Phase마다 
    public CheckHpNode(BossBehaviourTree bossBehaviourTree, int totalPhase)
    {
        _bossBehaviourTree = bossBehaviourTree;
        _totalPhase = totalPhase;
    }

    public override NodeState Evaluate()
    {
        float nextPhaseHealth = _bossBehaviourTree.StatHandler.Data.MaxHealth * (_totalPhase - _currentPhase) / _totalPhase ;

        if (nextPhaseHealth > _bossBehaviourTree.StatHandler.Data.Health)
        {
            if (_bossBehaviourTree.StatHandler.Data.Health <= 0) 
            {
                state = NodeState.Failure;
                return state;
            }

            _currentPhase++;
            _currentPhase = _currentPhase > _totalPhase ? _totalPhase : _currentPhase;
            _bossBehaviourTree.SetCurrenPhase(_currentPhase);
        }

        state = NodeState.Success;
        return state;
    }
}
