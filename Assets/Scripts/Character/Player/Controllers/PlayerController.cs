using UnityEngine;

public abstract class PlayerController : EntityController
{
    [SerializeField] private PlayerSO stat;

    public PlayerStatHandler StatHandler { get; private set; }

    public PlayerStateMachine StateMachine { get; protected set; }

    [field: SerializeField] public MovementData MovementData { get; private set; }

    public PlayerAnimationController AnimationController { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        StatHandler = new PlayerStatHandler(stat);
        AnimationController = GetComponentInChildren<PlayerAnimationController>();
        AnimationController.Init();
    }

    protected void Start()
    {
        StateMachine.Init();
        StatHandler.DamagedAction += OnDamaged;
        StatHandler.DieAction += StateMachine.Ondead;
    }

    protected virtual void Update()
    {
        StateMachine.Update();
        StatHandler.Recovery(StatHandler.Data.HealthRegen * Time.deltaTime);
    }

    protected void FixedUpdate()
    {
        StateMachine.PhysicsUpdate();
    }

    protected void OnDestroy()
    {
        StatHandler.DamagedAction -= OnDamaged;
        StatHandler.DieAction -= StateMachine.Ondead;
    }

    public override void OnDamaged()
    {
        if (StateMachine.IsDead)
            return;
        base.OnDamaged();
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Vector3 offsetPos = new Vector3(transform.position.x, transform.position.y + GroundData.GroundYOffset, transform.position.z); ;

        Gizmos.DrawSphere(offsetPos, GroundData.GroundYOffset * GroundData.GroundRadiusMod);
    }

#endif

}
