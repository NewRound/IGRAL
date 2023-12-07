public class PlayerMoveState : PlayerStateBase
{
    public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine)
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
        if (!stateMachine.GroundDataHandler.IsGrounded && !stateMachine.IsAttacking)
            stateMachine.ChangeState(stateMachine.FallState);
    }

    public override void PhysicsUpdateState()
    {
        base.PhysicsUpdateState();
    }

    public override void Exit()
    {
        animationController.PlayAnimation(animationsData.MoveSubStateParameterHash, false);
    }

    public override void OnDead()
    {
        base.OnDead();
    }
}