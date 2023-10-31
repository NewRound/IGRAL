public class PlayerFallState : PlayerAirState
{
    public PlayerFallState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        animationController.PlayAnimation(animationsData.FallParameterHash, true);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (stateMachine.GroundDataHandler.IsGrounded)
            stateMachine.ChangeState(stateMachine.MoveState);
    }

    public override void Exit()
    {
        base.Exit();
        animationController.PlayAnimation(animationsData.FallParameterHash, false);
    }
}
