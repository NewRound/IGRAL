using UnityEngine;

public abstract class EnemyMoveState : EnemyStateBase
{
    protected EnemyMoveState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        animationController.PlayAnimation(animationsData.MoveSubStateParameterHash, true);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        animationController.PlayAnimation(animationsData.SpeedRatioParameterHash, stateMachine.SpeedRatio);
    }

    public override void Exit()
    {
        animationController.PlayAnimation(animationsData.MoveSubStateParameterHash, false);
        stateMachine.SetDirection(Vector2.zero);
    }
}