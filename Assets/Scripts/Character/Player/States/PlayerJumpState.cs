using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    private bool _isAttacked = false;

    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        InputController.JumpAction += Jump;
        InputController.AttackAction += OnAttackInputted;
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

    public override void UpdateState()
    {
        base.UpdateState();

        if (_isAttacked)
            stateMachine.ChangeState(stateMachine.ComboAttackState);
    }

    public override void PhysicsUpdateState()
    {
        base.PhysicsUpdateState();

        if (stateMachine.Rigid.velocity.y < 0)
            stateMachine.ChangeState(stateMachine.FallState);
    }

    public override void OnDead()
    {
        base.OnDead();
        InputController.JumpAction -= Jump;
        InputController.AttackAction -= OnAttackInputted;
    }

    private void Jump()
    {
        if (stateMachine.JumpCountHandler.JumpCount > 0)
        {
            AnimationUtil.ReStartIfAnimationIsPlaying(animationController.Animator, animationsData.JumpParameterHash);
            stateMachine.JumpCountHandler.DecreaseJumpCount();
            Vector3 velocity = stateMachine.Rigid.velocity;
            velocity.y = InputController.StatHandler.Data.JumpingForce;
            stateMachine.Rigid.velocity = velocity;
            
            if(stateMachine.JumpCountHandler.JumpCount == 0)
            {
                EffectManager.Instance.ShowEffect(GameManager.Instance.PlayerTransform.position, EffectType.PlayerJump);
            }
        }

    }

    private void OnAttackInputted()
    {
        _isAttacked = true;
    }
}
