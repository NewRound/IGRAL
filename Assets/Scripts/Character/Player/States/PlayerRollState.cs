public class PlayerRollState : PlayerStateBase
{
    public PlayerRollState(StateMachine stateMachine) : base(stateMachine)
    {
        playerController.RollAction += stateMachine.RollDataHandler.ResetCurrentRollingElapsedTime;
    }

    public override void Enter()
    {
        animationController.PlayAnimation(animationsData.RollParameterHash);
        stateMachine.RollDataHandler.SetIsRolling(true);
    }

    public override void UpdateState()
    {
        CheckRollingEnded();
        base.UpdateState();
    }

    private void CheckRollingEnded()
    {
        if (animationController.CheckAnimationEnded(animationsData.RollParameterHash))
        {
            stateMachine.RollDataHandler.SetIsRolling(false);

            if (stateMachine.IsGrounded)
                stateMachine.ChangeState(stateMachine.MovementState);
            else
                stateMachine.ChangeState(stateMachine.FallState);
        }
    }

    public override void PhysicsUpdateState()
    {
        base.PhysicsUpdateState();

    }

    public override void Exit()
    {
    }

    public override void OnDead()
    {
        base.OnDead();
        playerController.RollAction -= stateMachine.RollDataHandler.ResetCurrentRollingElapsedTime;
    }

}
