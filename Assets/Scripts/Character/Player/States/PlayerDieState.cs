using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerDieState : PlayerStateBase
{
    public PlayerDieState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        if (stateMachine.IsDead)
            return;

        stateMachine.SetDead(true);
        animationController.PlayAnimation(animationsData.dieParameterHash, true);
    }

    public override void UpdateState()
    {
        if (!stateMachine.IsDead)
            stateMachine.ChangeState(stateMachine.MoveState);

    }

    public override void Exit()
    {
        stateMachine.SetDead(false);
        animationController.PlayAnimation(animationsData.dieParameterHash, false);
    }
}
