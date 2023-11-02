using UnityEngine;

public class EnemyAttackState : EnemyStateBase
{
    public EnemyAttackState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();
        stateMachine.CheckAttackRange();
        if (!stateMachine.IsAttacking)
        {
            if (stateMachine.IsTracing)
                stateMachine.ChangeState(stateMachine.TraceState);
            else
                stateMachine.ChangeState(stateMachine.PatrolState);
        }
    }

    public override void Exit()
    {
    }
}
