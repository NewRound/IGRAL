using UnityEngine;

public abstract class EnemyStateBase : StateBase
{
    protected EnemyStateMachine stateMachine;
    protected EnemyController enemyController;
    protected AnimationController animationController;

    protected EnemyAnimationsData animationsData;

    public EnemyStateBase(EnemyStateMachine enemyStateMachine)
    {
        stateMachine = enemyStateMachine;
        enemyController = enemyStateMachine.EnemyController;

        animationController = enemyController.AnimationController;
        animationsData = enemyController.AnimationController.AnimationData;
    }

    public override void UpdateState()
    {
        stateMachine.UpdateSpeed();
        stateMachine.Look();
    }

    public override void PhysicsUpdateState()
    {
        stateMachine.Move();
    }
}
