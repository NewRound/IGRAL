using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public event Action<Vector2> MoveAction;

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

    private void CallMoveAction(Vector2 inputVec)
    {
        MoveAction?.Invoke(inputVec);
    }
}
