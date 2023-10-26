using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : InputController
{
    [SerializeField] private PlayerSO stat;

    public PlayerStatHandler StatHandler { get; private set; }

    public Rigidbody Rigidbody { get; private set; }
    public Transform Transform { get; private set; }
    public Animator Animator { get; private set; }

    // TODO : StateMachine 들어갈 자리
    private StateMachine _stateMachine;

    [field: SerializeField] public Movement Movement { get; private set; }
    [field: SerializeField] public PlayerAnimationsData AnimationData { get; private set; }

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

}
