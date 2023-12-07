

public abstract class PlayerAttackState : PlayerMoveState
{
    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.SetIsAttacking(true);
        animationController.PlayAnimation(animationsData.AttackSubStateParameterHash, true);
        stateMachine.LookPreDirectionRightAway();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void Exit()
    {
        stateMachine.SetIsAttacking(false);
    }

    public override void OnDead()
    {
        base.OnDead();
    }
}
