using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : InputController
{
    [SerializeField] private PlayerSO stat;

    public PlayerStatHandler StatHandler { get; private set; }

    public Rigidbody Rigidbody { get; private set; }
    public Transform Transform { get; private set; }
    public Animator Animator { get; private set; }

    private StateMachine _stateMachine;


    [field: SerializeField] public Movement Movement { get; private set; }
    [field: SerializeField] public PlayerAnimationsData AnimationData { get; private set; }
    [field: SerializeField] public GroundCheck GroundCheck { get; private set; }


    protected override void Awake()
    {
        base.Awake();
        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();


        _stateMachine = new StateMachine(this);
        StatHandler = new PlayerStatHandler(stat);
    }

    private void Start()
    {
        GroundCheck.Init(transform);
        AnimationData.Init();
        _stateMachine.Init();

        Transform = transform;
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.PhysicsUpdate();
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Vector3 offsetPos = new Vector3(transform.position.x, transform.position.y + GroundCheck.GroundYOffset, transform.position.z); ;

        Gizmos.DrawSphere(offsetPos, GroundCheck.GroundYOffset * GroundCheck.GroundRadiusMod);
    }

#endif

}
