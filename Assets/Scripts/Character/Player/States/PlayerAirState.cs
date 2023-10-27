public abstract class PlayerAirState : PlayerStateBase
{
    protected JumpCountHandler jumpCountSetter;

    public PlayerAirState(StateMachine stateMachine) : base(stateMachine)
    {
        jumpCountSetter = stateMachine.JumpCountSetter;
    }


    public override void Enter()
    {
        playerController.Animator.SetBool(animationsData.AirParameterHash, true);
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void Exit()
    {
        playerController.Animator.SetBool(animationsData.AirParameterHash, false);
    }
}
