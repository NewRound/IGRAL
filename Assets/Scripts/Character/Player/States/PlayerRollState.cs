using UnityEngine;

public class PlayerRollState : PlayerStateBase
{
    public PlayerRollState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        InputController.RollAction += stateMachine.RollDataHandler.ResetCurrentRollingElapsedTime;
        InputController.RollAction += stateMachine.MovementDataHandler.LookPreDirectionRightAway;
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

            if (stateMachine.GroundDataHandler.IsGrounded)
                stateMachine.ChangeState(stateMachine.MoveState);
            else
                stateMachine.ChangeState(stateMachine.FallState);
        }
    }

    public override void PhysicsUpdateState()
    {
        base.PhysicsUpdateState();
        if (stateMachine.RollDataHandler.IsRolling)
            Roll();
    }

    public override void Exit()
    {
    }

    public override void OnDead()
    {
        base.OnDead();
        InputController.RollAction -= stateMachine.RollDataHandler.ResetCurrentRollingElapsedTime;
        InputController.RollAction -= stateMachine.MovementDataHandler.LookPreDirectionRightAway;
    }

    private void Roll()
    {
        Vector3 velocity = movementDataHandler.Rigid.velocity;
        velocity.x = movementDataHandler.PreDirection.x >= 0 ? InputController.StatHandler.Data.RollingForce : -InputController.StatHandler.Data.RollingForce;
        movementDataHandler.Rigid.velocity = velocity;
    }

}
