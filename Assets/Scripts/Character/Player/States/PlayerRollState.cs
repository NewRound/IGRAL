public class PlayerRollState : PlayerStateBase
{
    public PlayerRollState(StateMachine stateMachine) : base(stateMachine)
    {
        playerController.RollAction += stateMachine.RollCoolTimeCalculator.ResetCurrentRollingElapsedTime;
    }

    public override void Enter()
    {
        animationController.PlayAnimation(animationsData.RollParameterHash);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (animationController.CheckAnimationEnded(animationsData.RollParameterHash))
        {
            if (stateMachine.IsGrounded)
                stateMachine.ChangeState(stateMachine.MovementState);
            else
                stateMachine.ChangeState(stateMachine.FallState);
        }
    }

    public override void Exit()
    {
    }

    public override void OnDead()
    {
        base.OnDead();
        playerController.RollAction -= stateMachine.RollCoolTimeCalculator.ResetCurrentRollingElapsedTime;
    }

}
