public class PlayerMoveState : PlayerStateBase
{
    public PlayerMoveState(StateMachine stateMachine) : base(stateMachine)
    {
        
    }

    public override void Enter()
    {
        PlayAnimation(animationsData.MoveParameterHash, true);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        PlayAnimation(animationsData.SpeedRatioParameterHash, speedRatio);
    }

    public override void Exit()
    {
        PlayAnimation(animationsData.MoveParameterHash, false);
    }

    public override void OnDead()
    {
        base.OnDead();
    }
}