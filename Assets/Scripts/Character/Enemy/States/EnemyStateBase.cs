public abstract class EnemyStateBase : StateBase
{
    protected EnemyStateMachine stateMachine;
    protected EnemyController enemyController;
    protected EnemyMovementDataHandler movementDataHandler;

    public EnemyStateBase(EnemyStateMachine enemyStateMachine)
    {
        stateMachine = enemyStateMachine;
        enemyController = enemyStateMachine.EnemyController;
        movementDataHandler = enemyStateMachine.MovementDataHandler;

        animationController = enemyController.AnimationController;
        animationsData = enemyController.AnimationController.AnimationData;
    }

    public override void UpdateState()
    {
        stateMachine.MovementDataHandler.UpdateSpeed();
        stateMachine.MovementDataHandler.Look();
    }

    public override void PhysicsUpdateState()
    {
        stateMachine.MovementDataHandler.Move();
    }
}
