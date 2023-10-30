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

#if UNITY_EDITOR
    [field: SerializeField] public bool IsDebug { get; private set; }
    private bool _isMovePressed;
#endif

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
#if UNITY_EDITOR
        if (IsDebug)
            ReadMoveInput();
#endif
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
        MoveAction?.Invoke(inputVec);
    }

    public void CallJumpAction()
    {
        if (stateMachine.RollDataHandler.IsRolling)
            return;

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
        if (stateMachine.RollDataHandler.IsRolling)
            return;

        AttackAction?.Invoke();
    }

#if UNITY_EDITOR
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
#endif

}
