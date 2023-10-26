using System;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class InputController : MonoBehaviour
{
    public event Action<Vector2> MoveAction;
    public event Action JumpAction;
    public event Action SlideAction;
    public event Action AttackAction;

    public PlayerInput Input { get; private set; }
    public PlayerInputAction InputActions { get; private set; }
    public PlayerInputAction.PlayerActions PlayerActions { get; private set; }


    protected virtual void Awake()
    {
        Input = GetComponent<PlayerInput>();

        InputActions = new PlayerInputAction();
        PlayerActions = InputActions.Player;
    }

    private void OnEnable()
    {
        InputActions.Enable();
    }

    private void OnDisable()
    {
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
        if (context.performed)
        {
            CallJumpAction();
        }
    }

    public void OnSlide(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CallSlideAction();
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CallAttackAction();
        }
    }

    public void CallMoveAction(Vector2 inputVec)
    {
        MoveAction?.Invoke(inputVec);
    }

    public void CallJumpAction()
    {
        JumpAction?.Invoke();
    }

    public void CallSlideAction()
    {
        SlideAction?.Invoke();
    }

    public void CallAttackAction()
    {
        AttackAction?.Invoke();
    }
}
