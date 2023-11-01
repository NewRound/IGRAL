using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementDataHandler : MovementDataHandler
{
    private RollDataHandler _rollDataHandler;

    public PlayerMovementDataHandler(MovementData movementData, RollDataHandler rollDataHandler, Rigidbody rigidbody, float speedMin, float speedMax) : base(movementData, rigidbody, speedMin, speedMax)
    {
        _rollDataHandler = rollDataHandler;
    }

    public override void Look()
    {
        if (_rollDataHandler.IsRolling)
            return;

        if (direction.x == 0)
        {
            LookPreDirectionRightAway();
            return;
        }

        base.Look();
    }
}
