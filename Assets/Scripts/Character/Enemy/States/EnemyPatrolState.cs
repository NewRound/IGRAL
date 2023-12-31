public class EnemyPatrolState : EnemyMoveState
{
    public EnemyPatrolState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.CheckArrived();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (stateMachine.PlayerStateMachine.IsDead)
            return;

        if (stateMachine.IsTracing)
        {
            stateMachine.ChangeState(stateMachine.TraceState);
            return;
        }

    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.StopCheckingArrived();
    }
}
