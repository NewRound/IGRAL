using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public EnemyController EnemyController { get; private set; }

    public EnemyPatrolState PatrolState { get; private set; }
    public EnemyTraceState TraceState { get; private set; }

    public EnemyMovementDataHandler MovementDataHandler { get; private set; }

    public TraceDataHandler TraceDataHandler { get; private set; }

    public Transform PlayerTransform { get; private set; }

    public EnemyStateMachine(EnemyController controller)
    {
        EnemyController = controller;

        MovementDataHandler = new EnemyMovementDataHandler(
            EnemyController.MovementData,
            TraceDataHandler,
            EnemyController.StatHandler.Data.SpeedMin,
            EnemyController.StatHandler.Data.SpeedMax,
            EnemyController.Rigidbody);

        PlayerTransform = GameManager.Instance.player.transform;

        TraceDataHandler = new TraceDataHandler(EnemyController.TraceData, EnemyController.transform, PlayerTransform);

        PatrolState = new EnemyPatrolState(this);

    }

    public override void Init()
    {
        ChangeState(PatrolState);
    }

    public override void Update()
    {
        TraceDataHandler.CalculateDistance();
        base.Update();
    }
}
