using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public event Action<Vector2> MoveAction;
    public event Action JumpAction;
    public event Action SlideAction;
    public event Action AttackAction;

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

    private void CallMoveAction(Vector2 inputVec)
    {
        MoveAction?.Invoke(inputVec);
    }

    private void CallJumpAction()
    {
        JumpAction?.Invoke();
    }

    private void CallSlideAction()
    {
        SlideAction?.Invoke();
    }

    private void CallAttackAction()
    {
        AttackAction?.Invoke();
    }
}
