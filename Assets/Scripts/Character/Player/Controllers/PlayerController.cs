using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : InputController
{
    [SerializeField] private PlayerSO stat;

    public PlayerStatHandler StatHandler { get; private set; }
    public AnimationController AnimationController { get; private set; }

    public Rigidbody Rigidbody { get; private set; }

    [field: SerializeField] public MovementData MovementData { get; private set; }
    [field: SerializeField] public GroundCheck GroundCheck { get; private set; }


    protected override void Awake()
    {
        base.Awake();
        Rigidbody = GetComponent<Rigidbody>();
        AnimationController = GetComponentInChildren<AnimationController>();
        AnimationController.Init();

        StatHandler = new PlayerStatHandler(stat);
        stateMachine = new StateMachine(this);
    }

    private void Start()
    {
        transform.forward = Vector3.right;

        GroundCheck.Init(transform);
        stateMachine.Init();
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Vector3 offsetPos = new Vector3(transform.position.x, transform.position.y + GroundCheck.GroundYOffset, transform.position.z); ;

        Gizmos.DrawSphere(offsetPos, GroundCheck.GroundYOffset * GroundCheck.GroundRadiusMod);
    }

#endif

}
