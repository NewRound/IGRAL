using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playerController.Animator.SetBool(animationsData.JumpParameterHash, true);
    }

    public override void Exit()
    {
        base.Exit();
        playerController.Animator.SetBool(animationsData.JumpParameterHash, false);
    }
}
