using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        InputController.JumpAction += Jump;
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
        InputController.JumpAction -= Jump;
    }

    private void Jump()
    {
        if (stateMachine.JumpCountHandler.JumpCount > 0)
        {
            animationController.ReStartIfAnimationIsPlaying(animationsData.JumpParameterHash);

            stateMachine.JumpCountHandler.DecreaseJumpCount();
            Vector3 velocity = movementDataHandler.Rigid.velocity;
            velocity.y = InputController.StatHandler.Data.JumpingForce;
            movementDataHandler.Rigid.velocity = velocity;
        }
    }
}
