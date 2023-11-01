using System;
using System.Collections;
using UnityEngine;

public class EnemyController : CharacterController
{
    [SerializeField] private EnemySO stat;

    public EnemyStatHandler StatHandler { get; private set; }

    private EnemyStateMachine _stateMachine;

    [field: SerializeField] public EnemyMovementData MovementData { get; private set; }
    
    protected override void Awake()
    {
        base.Awake();
        StatHandler = new EnemyStatHandler(stat);
        _stateMachine = new EnemyStateMachine(this);
    }

    private void Start()
    {
        _stateMachine.Init();
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.PhysicsUpdate();
    }

}
