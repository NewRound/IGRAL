﻿public class PlayerMoveState : PlayerStateBase
{
    public PlayerMoveState(StateMachine stateMachine) : base(stateMachine)
    {
        
    }

    public override void Enter()
    {
        animationController.PlayAnimation(animationsData.MoveParameterHash, true);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        animationController.PlayAnimation(animationsData.SpeedRatioParameterHash, speedRatio);
    }

    public override void Exit()
    {
        animationController.PlayAnimation(animationsData.MoveParameterHash, false);
    }

    public override void OnDead()
    {
        base.OnDead();
    }
}