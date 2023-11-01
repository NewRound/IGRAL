using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public EnemyController EnemyController { get; private set; }

    public EnemyPatrolState PatrolState { get; private set; }
    public EnemyTraceState TraceState { get; private set; }

    public EnemyMovementDataHandler MovementDataHandler { get; private set; }

    public Transform PlayerTransform { get; private set; }

    public bool IsAttacking { get; private set; }

    public EnemyStateMachine(EnemyController controller)
    {
        EnemyController = controller;

        MovementDataHandler = new EnemyMovementDataHandler(
            EnemyController.MovementData,
            EnemyController.Rigidbody,
            EnemyController.transform,
            EnemyController.StatHandler.Data.SpeedMin,
            EnemyController.StatHandler.Data.SpeedMax
            );

        //PlayerTransform = GameManager.Instance.player.transform;

        PatrolState = new EnemyPatrolState(this);
    }

    public override void Init()
    {
        ChangeState(PatrolState);
    }

}
