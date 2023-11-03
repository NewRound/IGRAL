

public class PlayerAttackState : PlayerStateBase
{
    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        InputController.AttackAction += stateMachine.OnAttackInput;
        InputController.AttackAction += animationController.OnAttackInputted;
    }

    public override void Enter()
    {
        animationController.PlayAnimation(animationsData.AttackSubStateParameterHash, true);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (animationController.CheckCurrentClipEnded(animationController.AttackType))
        {
            stateMachine.ChangeState(stateMachine.MoveState);
        }
        else
        {
            if (animationController.IsAttackInputted && animationController.CheckCurrentClipEqual(animationController.AttackType))
            {
                animationController.SetAttackInputted(false);
                animationController.IncreaseAttackCombo();
                animationController.SetNextAttackType();
                animationController.PlayAnimation(animationsData.AttackComboHash, animationController.AttackCombo);
            }
        }
    }

    public override void Exit()
    {
        animationController.ResetCombo();
        animationController.ReSetAttackType();
        animationController.PlayAnimation(animationsData.AttackSubStateParameterHash, false);
        animationController.PlayAnimation(animationsData.AttackComboHash, animationController.AttackCombo);
    }

    public override void OnDead()
    {
        base.OnDead();
        InputController.AttackAction -= stateMachine.OnAttackInput;
        InputController.AttackAction -= animationController.OnAttackInputted;
    }
}
