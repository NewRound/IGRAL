public abstract class PlayerAirState : PlayerStateBase
{
    public PlayerAirState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        playerController.Animator.SetBool(animationsData.AirParameterHash, true);
    }

    public override void Exit()
    {
        playerController.Animator.SetBool(animationsData.AirParameterHash, false);
    }
}
