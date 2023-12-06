using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : PlayerController
{
#if UNITY_WEBGL
    private bool _isMovePressed;
#endif

    [field: Header("Actions")]
    public event Action<Vector2> MoveAction;
    public event Action JumpAction;
    public event Action RollAction;
    public event Action AttackAction;
    public event Action UseItemAction;
    public event Action InteractAction;
    public event Action QAction;
    public event Action WAction;
    public event Action EAction;
    public event Action RAction;

    [field: Header("Inputs")]
    public PlayerInput Input { get; private set; }
    public PlayerInputAction InputActions { get; private set; }
    public PlayerInputAction.PlayerActions PlayerActions { get; private set; }

    public Transform DronePos;

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
        InputActions.Player.UseItem.started += OnUseItem;
        InputActions.Player.Interact.started += OnInteract;
        InputActions.Player.Q.started += OnQ;
        InputActions.Player.W.started += OnW;
        InputActions.Player.E.started += OnE;
        InputActions.Player.R.started += OnR;
    }

    

    private void OnDisable()
    {
        InputActions.Player.Move.started -= OnMove;
        InputActions.Player.Move.canceled -= OnMove;
        InputActions.Player.Jump.started -= OnJump;
        InputActions.Player.Roll.started -= OnRoll;
        InputActions.Player.Attack.started -= OnAttack;
        InputActions.Player.UseItem.started -= OnUseItem;
        InputActions.Player.Interact.started -= OnInteract;
        InputActions.Player.Q.started -= OnQ;
        InputActions.Player.W.started -= OnW;
        InputActions.Player.E.started -= OnE;
        InputActions.Player.R.started -= OnR;
        InputActions.Disable();
    }

    protected override void Update()
    {
        base.Update();
#if UNITY_WEBGL
            ReadMoveInput();
#endif
    }

    public void OnMove(InputAction.CallbackContext context)
    {
#if UNITY_WEBGL
        _isMovePressed = context.started;
#endif
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

    public void OnUseItem(InputAction.CallbackContext context)
    {
        CallUseItemAction();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        CallInteractAction();
    }

    public void OnQ(InputAction.CallbackContext context)
    {
        CallOnQAction();
    }

    public void OnW(InputAction.CallbackContext context)
    {
        CallOnWAction();
    }

    public void OnE(InputAction.CallbackContext context)
    {
        CallOnEAction();
    }

    public void OnR(InputAction.CallbackContext context)
    {
        CallOnRAction();
    }

    public void CallMoveAction(Vector2 inputVec)
    {
        MoveAction?.Invoke(inputVec);
    }

    public void CallJumpAction()
    {
        if (StateMachine.RollDataHandler.IsRolling || StateMachine.CurrentState == StateMachine.ComboAttackState)
            return;

        StateMachine.ChangeState(StateMachine.JumpState);
        JumpAction?.Invoke();
    }

    public void CallRollAction()
    {
        if (!StateMachine.RollDataHandler.CanRoll)
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

    private void CallInteractAction()
    {
        InteractAction?.Invoke();
        UIController.Instance.OnInteractionButton();
        UIController.Instance.OnPickupButton();
        Debug.Log("Å° ÀÔ·Â");
    }

    private void CallUseItemAction()
    {
        UseItemAction?.Invoke();
        UIController.Instance.OnItemButton();
    }

    private void CallOnQAction()
    {
        QAction?.Invoke();
        SkillManager.Instance.skillUse[0].UseSkill();
    }

    private void CallOnWAction()
    {
        WAction?.Invoke();
        SkillManager.Instance.skillUse[1].UseSkill();
    }

    private void CallOnEAction()
    {
        EAction?.Invoke();
        SkillManager.Instance.skillUse[2].UseSkill();
    }

    private void CallOnRAction()
    {
        RAction?.Invoke();
        SkillManager.Instance.skillUse[3].UseSkill();
    }

#if UNITY_WEBGL
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
