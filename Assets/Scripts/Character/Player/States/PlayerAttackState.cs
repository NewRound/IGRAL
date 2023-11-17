

public abstract class PlayerAttackState : PlayerMoveState
{
    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        animationController.PlayAnimation(animationsData.AttackSubStateParameterHash, true);
        stateMachine.LookPreDirectionRightAway();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void Exit()
    {
    }

    public override void OnDead()
    {
        base.OnDead();
    }
}
