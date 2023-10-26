public class PlayerMoveState : PlayerStateBase
{
    public PlayerMoveState(StateMachine stateMachine) : base(stateMachine)
    {
        
    }

    public override void Enter()
    {
        playerController.Animator.SetBool(animationsData.MoveParameterHash, true);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        playerController.Animator.SetFloat(animationsData.SpeedRatioParameterHash, speedRatio);
    }

    public override void Exit()
    {
        playerController.Animator.SetBool(animationsData.MoveParameterHash, false);
    }

    public override void OnDead()
    {
        base.OnDead();
    }

}