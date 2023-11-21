using GlobalEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHpNode : Node
{
    private EnemyStatHandler _statHandler;
    private int _totalPhase;
    private int _currentPhase = 1;

    // PhaseSO�� �ٲ���� �� Phase���� 
    public CheckHpNode(EnemyStatHandler enemyStatHandler, int totalPhase)
    {
        _statHandler = enemyStatHandler;
        _totalPhase = totalPhase;
    }

    public override NodeState Evaluate()
    {
        float nextPhaseHealth = (_totalPhase - _currentPhase) / _totalPhase * _statHandler.Data.MaxHealth;

        if (nextPhaseHealth > _statHandler.Data.Health)
        {
            if (_statHandler.Data.Health <= 0) 
            {
                state = NodeState.Failure;
                return state;
            }

            _currentPhase++;
            _currentPhase = _currentPhase > _totalPhase ? _totalPhase : _currentPhase;
        }

        state = NodeState.Success;
        return state;
    }
}
