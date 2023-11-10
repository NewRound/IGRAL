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
        if (stateMachine.PlayerStateMachine.IsDead)
            return;

        base.UpdateState();

        if (stateMachine.IsTracing)
        {
            stateMachine.CheckAttackRange();

            if (stateMachine.IsAttacking)
            {
                stateMachine.ChangeState(stateMachine.AttackState);
                return;
            }

            stateMachine.TracePlayer();
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
