using System;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class InputController : MonoBehaviour
{
    public event Action<Vector2> MoveAction;
    public event Action JumpAction;
    public event Action RollAction;
    public event Action AttackAction;

    public PlayerInput Input { get; private set; }
    public PlayerInputAction InputActions { get; private set; }
    public PlayerInputAction.PlayerActions PlayerActions { get; private set; }

    protected StateMachine stateMachine;

    protected virtual void Awake()
    {
        Input = GetComponent<PlayerInput>();

        InputActions = new PlayerInputAction();
        PlayerActions = InputActions.Player;
    }

    private void OnEnable()
    {
        InputActions.Enable();
        InputActions.Player.Move.started += OnMove;
        InputActions.Player.Move.canceled += OnMove;
        InputActions.Player.Jump.started += OnJump;
        InputActions.Player.Roll.started += OnRoll;
    }

    private void OnDisable()
    {
        InputActions.Player.Move.started -= OnMove;
        InputActions.Player.Move.canceled -= OnMove;
        InputActions.Player.Jump.started -= OnJump;
        InputActions.Player.Roll.started -= OnRoll;
        InputActions.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            CallMoveAction(Vector2.zero);
            return;
        }

        Vector2 inputVec = context.ReadValue<Vector2>();
        
        CallMoveAction(inputVec);
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        CallJumpAction();
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        CallRollAction();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        CallAttackAction();
    }

    public void CallMoveAction(Vector2 inputVec)
    {
        MoveAction?.Invoke(inputVec);
    }

    public void CallJumpAction()
    {
        stateMachine.ChangeState(stateMachine.JumpState);
        JumpAction?.Invoke();
    }

    public void CallRollAction()
    {
        if (!stateMachine.RollCoolTimeCalculator.CanRoll)
            return;

        stateMachine.ChangeState(stateMachine.RollState);
        RollAction?.Invoke();
    }

    public void CallAttackAction()
    {
        AttackAction?.Invoke();
    }
}
