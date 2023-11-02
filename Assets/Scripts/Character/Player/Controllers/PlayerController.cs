using UnityEngine;

public abstract class PlayerController : CharacterController
{
    [SerializeField] private PlayerSO stat;

    public PlayerStatHandler StatHandler { get; private set; }

    protected PlayerStateMachine StateMachine { get; set; }

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
    }

    protected virtual void Update()
    {
        StateMachine.Update();
    }

    protected void FixedUpdate()
    {
        StateMachine.PhysicsUpdate();
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Vector3 offsetPos = new Vector3(transform.position.x, transform.position.y + GroundData.GroundYOffset, transform.position.z); ;

        Gizmos.DrawSphere(offsetPos, GroundData.GroundYOffset * GroundData.GroundRadiusMod);
    }

#endif

}
