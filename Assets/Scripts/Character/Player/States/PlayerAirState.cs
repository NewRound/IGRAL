public abstract class PlayerAirState : PlayerStateBase
{
    protected JumpCountHandler jumpCountSetter;

    public PlayerAirState(StateMachine stateMachine) : base(stateMachine)
    {
        jumpCountSetter = stateMachine.JumpCountSetter;
    }


    public override void Enter()
    {
        animationController.PlayAnimation(animationsData.AirParameterHash, true);
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void Exit()
    {
        animationController.PlayAnimation(animationsData.AirParameterHash, false);
    }
}
