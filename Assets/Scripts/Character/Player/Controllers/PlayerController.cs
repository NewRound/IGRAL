using UnityEngine;

public abstract class PlayerController : EntityController
{   
    private PlayerSO stat;

    public PlayerStatHandler StatHandler { get; private set; }

    public PlayerStateMachine StateMachine { get; protected set; }

    [field: SerializeField] public MovementData MovementData { get; private set; }

    public PlayerAnimationController AnimationController { get; private set; }

    public PlayerEffectController EffectController { get; private set; }

    private bool isDie;

    protected override void Awake()
    {
        base.Awake();
        EffectController = GetComponent<PlayerEffectController>();
        AnimationController = GetComponentInChildren<PlayerAnimationController>();
        AnimationController.Init();
        EffectController.EffectDataHandler.SetAuraMaterial(meshRenderer.sharedMaterials[1]);

        isDie = GameManager.Instance.isDie;

        stat = DataManager.Instance.playerSO;
        StatHandler = new PlayerStatHandler(stat);

    }

    protected void Start()
    {
        StateMachine.Init();
        StatHandler.DamagedAction += OnDamaged;
        StatHandler.DieAction += StateMachine.Ondead;
    }

    protected virtual void Update()
    {
        if (isDie)
            return;

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
