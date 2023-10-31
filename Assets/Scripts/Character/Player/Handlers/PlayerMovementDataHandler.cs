using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementDataHandler : MovementDataHandler
{
    private RollDataHandler _rollDataHandler;

    public PlayerMovementDataHandler(MovementData movementData, float speedMin, float speedMax, RollDataHandler rollDataHandler, Rigidbody rigidbody) : base(movementData, speedMin, speedMax, rigidbody)
    {
        _rollDataHandler = rollDataHandler;
    }

    public override void Look()
    {
        if (_rollDataHandler.IsRolling)
            return;

        base.Look();
    }

}
