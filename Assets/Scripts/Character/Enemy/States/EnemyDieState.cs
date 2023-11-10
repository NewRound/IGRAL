using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieState : EnemyStateBase
{
    public EnemyDieState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {

    }

    public override void Enter()
    {
        if (stateMachine.IsDead)
            return;

        stateMachine.SetDead(true);
        animationController.PlayAnimation(animationsData.dieParameterHash, true);
    }

    public override void UpdateState()
    {
        if (!stateMachine.IsDead)
            stateMachine.ChangeState(stateMachine.PatrolState);
    }

    public override void Exit()
    {
    }

    
}
