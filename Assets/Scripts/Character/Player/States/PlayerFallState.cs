public class PlayerFallState : PlayerAirState
{
    public PlayerFallState(StateMachine stateMachine) : base(stateMachine)
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

        if (stateMachine.IsGrounded)
            stateMachine.ChangeState(stateMachine.MovementState);
    }

    public override void Exit()
    {
        base.Exit();
        animationController.PlayAnimation(animationsData.FallParameterHash, false);
    }
}
