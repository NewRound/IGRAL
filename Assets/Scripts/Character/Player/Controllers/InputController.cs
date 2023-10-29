using System;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class InputController : MonoBehaviour
{
    public event Action<Vector2> MoveAction;
    public event Action JumpAction;
    public event Action RollAction;
    public event Action AttackAction;

    private bool _isMovePressed;

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

    protected virtual void Update()
    {
        ReadMoveInput();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _isMovePressed = context.started;
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
        //if (stateMachine.RollDataHandler.IsRolling)
        //    return;

        MoveAction?.Invoke(inputVec);
    }

    public void CallJumpAction()
    {
        stateMachine.ChangeState(stateMachine.JumpState);
        JumpAction?.Invoke();
    }

    public void CallRollAction()
    {
        if (!stateMachine.RollDataHandler.CanRoll)
            return;

        stateMachine.ChangeState(stateMachine.RollState);
        RollAction?.Invoke();
    }

    public void CallAttackAction()
    {
        AttackAction?.Invoke();
    }

    private void ReadMoveInput()
    {
        if (stateMachine.RollDataHandler.IsRolling)
            return;

        if (!_isMovePressed)
        {
            CallMoveAction(Vector2.zero);
            return;
        }

        CallMoveAction(InputActions.Player.Move.ReadValue<Vector2>());
    }
}
