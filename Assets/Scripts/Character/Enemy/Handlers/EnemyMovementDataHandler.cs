using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementDataHandler : MovementDataHandler
{
    private EnemyMovementData _movementData;
    private TraceDataHandler _traceDataHandler;

    public EnemyMovementDataHandler(EnemyMovementData movementData, TraceDataHandler traceDataHandler, float speedMin, float speedMax, Rigidbody rigidbody) : base(movementData, speedMin, speedMax, rigidbody)
    {
        _movementData = movementData;
        _traceDataHandler = traceDataHandler;
    }

    public override void UpdateSpeed()
    {
        base.UpdateSpeed();

        if (direction == Vector2.zero)
            return;

        if (!_traceDataHandler.IsTracing)
        {
            speedRatio = speedRatio > _movementData.PatrolAnimationRatio ? 
                _movementData.PatrolAnimationRatio : speedRatio;
        }
    }
}
