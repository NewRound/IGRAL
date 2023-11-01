using UnityEngine;

public class EnemyPatrolState : EnemyMoveState
{
    public EnemyPatrolState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        movementDataHandler.CheckArrivedTargetPos();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (movementDataHandler.IsTracing)
        {
            stateMachine.ChangeState(stateMachine.TraceState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        movementDataHandler.LookPreDirectionRightAway();
    }


}
