using System;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class InputController : MonoBehaviour
{
    [field: Header("Actions")]
    public event Action<Vector2> MoveAction;
    public event Action JumpAction;
    public event Action RollAction;
    public event Action AttackAction;

    [field: Header("Inputs")]
    public PlayerInput Input { get; private set; }
    public PlayerInputAction InputActions { get; private set; }
    public PlayerInputAction.PlayerActions PlayerActions { get; private set; }

    protected StateMachine stateMachine;

    private Vector2 _move;
    private bool _isRolling;

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
        _isRolling = stateMachine.RollDataHandler.IsRolling;
        if(_move != Vector2.zero)
        {
            CallMoveAction(_move);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _move = InputActions.Player.Move.ReadValue<Vector2>();
        if (_move == Vector2.zero)
        {
            CallMoveAction(_move);
        }
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
        if (_isRolling)
        {
            MoveAction?.Invoke(Vector2.zero);
            return;
        }

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

}
