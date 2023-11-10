using UnityEngine;

public abstract class PlayerController : CharacterController
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
        StatHandler.DieAction -= StateMachine.Ondead;
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Vector3 offsetPos = new Vector3(transform.position.x, transform.position.y + GroundData.GroundYOffset, transform.position.z); ;

        Gizmos.DrawSphere(offsetPos, GroundData.GroundYOffset * GroundData.GroundRadiusMod);
    }

#endif

}
