using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementDataHandler : MovementDataHandler
{
    public EnemyMovementDataHandler(MovementData movementData, float speedMin, float speedMax, Rigidbody rigidbody) : base(movementData, speedMin, speedMax, rigidbody)
    {
    }
}
