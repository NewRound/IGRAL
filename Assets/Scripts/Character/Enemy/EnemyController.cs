using System;
using System.Collections;
using UnityEngine;

public class EnemyController : CharacterController
{
    [SerializeField] private EnemySO stat;

    public EnemyStatHandler StatHandler { get; private set; }

    public EnemyStateMachine StateMachine {get; private set; }

    [field: SerializeField] public EnemyMovementData MovementData { get; private set; }
    
    protected override void Awake()
    {
        base.Awake();
        StatHandler = new EnemyStatHandler(stat);
        StateMachine = new EnemyStateMachine(this);
    }

    private void Start()
    {
        StateMachine.Init();
    }

    private void Update()
    {
        StateMachine.Update();
    }

    private void FixedUpdate()
    {
        StateMachine.PhysicsUpdate();
    }

    public void ExcuteCoroutine(IEnumerator enumerator)
    {
        StartCoroutine(enumerator);
    }

    public void TerminateCoroutine(IEnumerator enumerator)
    {
        StopCoroutine(enumerator);
    }
}
