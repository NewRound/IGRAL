using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyTraceState : EnemyMoveState
{
    public EnemyTraceState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (movementDataHandler.IsTracing)
        {
            //if (stateMachine.IsAttacking)
            //    stateMachine.ChangeState(stateMachine.AttackState);
        }
        else
        {
            stateMachine.ChangeState(stateMachine.PatrolState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
