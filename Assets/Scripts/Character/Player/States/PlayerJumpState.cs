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
        animationController.PlayAnimation(animationsData.JumpParameterHash, true);
    }

    public override void Exit()
    {
        base.Exit();
        animationController.PlayAnimation(animationsData.JumpParameterHash, false);
    }

    public override void PhysicsUpdateState()
    {
        base.PhysicsUpdateState();

        if (movementDataHandler.Rigid.velocity.y < 0)
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
            animationController.ReStartIfAnimationIsPlaying(animationsData.JumpParameterHash);

            jumpCountSetter.DecreaseJumpCount();
            Vector3 velocity = movementDataHandler.Rigid.velocity;
            velocity.y = playerController.StatHandler.Data.JumpingForce;
            movementDataHandler.Rigid.velocity = velocity;
        }
    }
}
