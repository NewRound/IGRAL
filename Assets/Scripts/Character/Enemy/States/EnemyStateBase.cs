public abstract class EnemyStateBase : StateBase
{
    protected EnemyStateMachine enemyStateMachine;
    protected EnemyController enemyController;
    protected MovementDataHandler movementDataHandler;

    public EnemyStateBase(EnemyStateMachine enemyStateMachine)
    {
        this.enemyStateMachine = enemyStateMachine;
        enemyController = enemyStateMachine.EnemyController;
        movementDataHandler = enemyStateMachine.MovementDataHandler;
    }

    public override void UpdateState()
    {
        enemyStateMachine.MovementDataHandler.UpdateSpeed();
        enemyStateMachine.MovementDataHandler.Look();
    }

    public override void PhysicsUpdateState()
    {
        enemyStateMachine.MovementDataHandler.Move();
    }
}
