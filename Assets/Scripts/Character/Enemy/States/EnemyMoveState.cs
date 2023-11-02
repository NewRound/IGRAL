using UnityEngine;

public abstract class EnemyMoveState : EnemyStateBase
{
    protected EnemyMoveState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        animationController.PlayAnimation(animationsData.MoveParameterHash, true);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        animationController.PlayAnimation(animationsData.SpeedRatioParameterHash, stateMachine.SpeedRatio);
    }

    public override void Exit()
    {
        animationController.PlayAnimation(animationsData.MoveParameterHash, false);
        stateMachine.SetDirection(Vector2.zero);
    }
}