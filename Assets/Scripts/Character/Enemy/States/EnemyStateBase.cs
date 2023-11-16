public abstract class EnemyStateBase : StateBase
{
    protected EnemyStateMachine stateMachine;
    protected EnemyController enemyController;
    protected EnemyAnimationController animationController;

    protected CharacterAnimationsData animationsData;

    public EnemyStateBase(EnemyStateMachine enemyStateMachine)
    {
        stateMachine = enemyStateMachine;
        enemyController = enemyStateMachine.EnemyController;

        animationController = enemyController.AnimationController;
        animationsData = enemyController.AnimationController.AnimationData;
    }

    public override void UpdateState()
    {
        if (stateMachine.PlayerStateMachine.IsDead && stateMachine.CurrentState != stateMachine.PatrolState)
        {
            stateMachine.ChangeState(stateMachine.PatrolState);
            return;
        }

        stateMachine.UpdateSpeed();
        stateMachine.Look();
    }

    public override void PhysicsUpdateState()
    {
        stateMachine.Move();
    }

    public override void OnDead()
    {
        base.OnDead();
        stateMachine.SetIsTracing(false);
        stateMachine.SetIsAttacking(false);
    }
}
