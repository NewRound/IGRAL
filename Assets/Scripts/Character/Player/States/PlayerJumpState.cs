using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(StateMachine stateMachine) : base(stateMachine)
    {
        playerController.JumpAction += Jump;
    }

    public override void Enter()
    {
        base.Enter();
        PlayAnimation(animationsData.JumpParameterHash, true);
    }

    public override void Exit()
    {
        base.Exit();
        PlayAnimation(animationsData.JumpParameterHash, false);
    }

    public override void PhysicsUpdateState()
    {
        base.PhysicsUpdateState();

        if (rigid.velocity.y < 0)
            stateMachine.ChangeState(stateMachine.FallState);
    }

    public override void OnDead()
    {
        base.OnDead();
        playerController.JumpAction -= Jump;
    }

    private void Jump()
    {
        if (jumpCountSetter.JumpCount > 0)
        {
            jumpCountSetter.DecreaseJumpCount();
            Vector3 velocity = rigid.velocity;
            velocity.y = playerController.StatHandler.Data.JumpForce;
            rigid.velocity = velocity;
        }

        Debug.Log(jumpCountSetter.JumpCount);
    }
}
