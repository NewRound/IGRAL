public abstract class PlayerAirState : PlayerStateBase
{
    public PlayerAirState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }


    public override void Enter()
    {
        animationController.PlayAnimation(animationsData.AirSubStateParameterHash, true);
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void Exit()
    {
        animationController.PlayAnimation(animationsData.AirSubStateParameterHash, false);
    }
}
