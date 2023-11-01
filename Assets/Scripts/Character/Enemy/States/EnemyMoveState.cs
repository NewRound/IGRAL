public abstract class EnemyMoveState : EnemyStateBase
{
    protected EnemyMoveState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        animationController.PlayAnimation(animationsData.MoveParameterHash, true);
    }

    public override void Exit()
    {
        animationController.PlayAnimation(animationsData.MoveParameterHash, false);
    }
}