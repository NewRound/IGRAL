using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboAttackState : PlayerAttackState
{
    private bool _wasNextComboInputted;
    private const float HALF = 0.5f;

    public PlayerComboAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _wasNextComboInputted = false;
        InputController.AttackAction += OnAttackInputted;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        float normalizeTime = animationController.GetNormalizeTime(AnimTag.Attack);

        if (normalizeTime >= 1f)
        {
            if (_wasNextComboInputted)
            {
                animationController.IncreaseCombo();
                animationController.PlayAnimation(animationsData.AttackComboHash, animationController.AttackCombo);
                stateMachine.ChangeState(stateMachine.ComboAttackState);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.MoveState);
            }
        }
    }

    private void OnAttackInputted()
    {
        float normalizeTime = animationController.GetNormalizeTime(AnimTag.Attack);

        if (normalizeTime >= HALF)
            _wasNextComboInputted = true;
    }

    public override void Exit()
    {
        base.Exit();

        InputController.AttackAction -= OnAttackInputted;
        if (!_wasNextComboInputted)
        {
            animationController.ResetCombo();
            animationController.PlayAnimation(animationsData.AttackComboHash, animationController.AttackCombo);
            animationController.PlayAnimation(animationsData.AttackSubStateParameterHash, false);
        }
    }

    public override void OnDead()
    {
        base.OnDead();
    }
}
