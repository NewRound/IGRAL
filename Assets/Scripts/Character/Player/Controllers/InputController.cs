using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : PlayerController
{
#if UNITY_EDITOR
    [field: SerializeField] public bool IsDebug { get; private set; }
    private bool _isMovePressed;
#endif

    [field: Header("Actions")]
    public event Action<Vector2> MoveAction;
    public event Action JumpAction;
    public event Action RollAction;
    public event Action AttackAction;

    [field: Header("Inputs")]
    public PlayerInput Input { get; private set; }
    public PlayerInputAction InputActions { get; private set; }
    public PlayerInputAction.PlayerActions PlayerActions { get; private set; }


    protected override void Awake()
    {
        base.Awake();
        Input = GetComponent<PlayerInput>();

        InputActions = new PlayerInputAction();
        StateMachine = new PlayerStateMachine(this);
        PlayerActions = InputActions.Player;
    }

    private void OnEnable()
    {
        InputActions.Enable();
        InputActions.Player.Move.started += OnMove;
        InputActions.Player.Move.canceled += OnMove;
        InputActions.Player.Jump.started += OnJump;
        InputActions.Player.Roll.started += OnRoll;
        InputActions.Player.Attack.started += OnAttack;
    }

    private void OnDisable()
    {
        InputActions.Player.Move.started -= OnMove;
        InputActions.Player.Move.canceled -= OnMove;
        InputActions.Player.Jump.started -= OnJump;
        InputActions.Player.Roll.started -= OnRoll;
        InputActions.Player.Attack.started -= OnAttack;
        InputActions.Disable();
    }

    protected override void Update()
    {
        base.Update();
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
        if (StateMachine.CurrentState == StateMachine.AttackState)
            return;

        MoveAction?.Invoke(inputVec);
    }

    public void CallJumpAction()
    {
        if (StateMachine.RollDataHandler.IsRolling || StateMachine.CurrentState == StateMachine.AttackState)
            return;

        StateMachine.ChangeState(StateMachine.JumpState);
        JumpAction?.Invoke();
    }

    public void CallRollAction()
    {
        if (!StateMachine.RollDataHandler.CanRoll || StateMachine.CurrentState == StateMachine.AttackState)
            return;

        StateMachine.ChangeState(StateMachine.RollState);
        RollAction?.Invoke();
    }

    public void CallAttackAction()
    {
        if (StateMachine.RollDataHandler.IsRolling)
            return;

        AttackAction?.Invoke();
    }

#if UNITY_EDITOR
    private void ReadMoveInput()
    {
        if (StateMachine.RollDataHandler.IsRolling)
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
