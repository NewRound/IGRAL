using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public EnemyController EnemyController { get; private set; }

    public EnemyPatrolState PatrolState { get; private set; }

    public MovementDataHandler MovementDataHandler { get; private set; }

    public Transform PlayerTransform { get; private set; }

    public EnemyStateMachine(EnemyController controller)
    {
        EnemyController = controller;

        MovementDataHandler = new MovementDataHandler(
            EnemyController.MovementData,
            EnemyController.StatHandler.Data.SpeedMin,
            EnemyController.StatHandler.Data.SpeedMax,
            EnemyController.Rigidbody);

        PatrolState = new EnemyPatrolState(this);

        PlayerTransform = GameManager.Instance.player.transform;
    }

    public override void Init()
    {
        ChangeState(PatrolState);
    }
}
