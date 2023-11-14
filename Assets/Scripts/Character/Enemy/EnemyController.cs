using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class EnemyController : EntityController
{
    [SerializeField] private EnemySO stat;
    
    public EnemyStatHandler StatHandler { get; private set; }

    public EnemyStateMachine StateMachine {get; private set; }

    [field: SerializeField] public EnemyMovementData MovementData { get; private set; }

    public EnemyAnimationController AnimationController { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        AnimationController = GetComponentInChildren<EnemyAnimationController>();
        AnimationController.Init();

        StatHandler = new EnemyStatHandler(stat);
        StateMachine = new EnemyStateMachine(this);

    }

    private void Start()
    {
        StateMachine.Init();
        StatHandler.DamagedAction += OnDamaged;
        StatHandler.DieAction += StateMachine.Ondead;
    }

    private void Update()
    {
        StateMachine.Update();
    }

    private void FixedUpdate()
    {
        StateMachine.PhysicsUpdate();
    }

    public override void OnDamaged()
    {
        if (StateMachine.IsDead)
            return;
        base.OnDamaged();
    }


    public void ExcuteCoroutine(IEnumerator enumerator)
    {
        StartCoroutine(enumerator);
    }

    public void TerminateCoroutine(IEnumerator enumerator)
    {
        StopCoroutine(enumerator);
    }

    

    private void OnDestroy()
    {
        StatHandler.DieAction -= StateMachine.Ondead;
        StatHandler.DamagedAction -= OnDamaged;
    }
}
