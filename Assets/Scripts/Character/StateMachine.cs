using UnityEngine;

public abstract class StateMachine
{
    protected IState currentState;

    public Vector3 PreDirection { get; private set; }
    public Vector2 Direction { get; private set; }

    public Rigidbody Rigid;

    [field: Header("Speed")]
    public SpeedCalculator SpeedCalculator { get; protected set; }
    public float Speed { get; protected set; }
    public float SpeedRatio { get => _speedRatio; protected set => _speedRatio = value; }
    private float _speedRatio;
    protected float speedMin;
    protected float speedMax;

    [field: Header("Rotation")]
    public RotationCalculator RotationCalculator { get; protected set; }
    public Transform ModelTrans { get; protected set; }

    public abstract void Init();

    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    public virtual void Update()
    {
        currentState.UpdateState();
    }

    public virtual void PhysicsUpdate()
    {
        currentState.PhysicsUpdateState();
    }

    public virtual void Look()
    {
        SetPreDirection(Direction.x * Vector3.right);

        ModelTrans.rotation = GetRotation();
    }

    public virtual void UpdateSpeed()
    {
        Speed = SpeedCalculator.CalculateSpeed(speedMin, speedMax, out _speedRatio, Direction == Vector2.zero);
    }

    public void SetPreDirection(Vector3 direction)
    {
        PreDirection = direction;
    }

    public void LookPreDirectionRightAway()
    {
        ModelTrans.rotation = Quaternion.LookRotation(PreDirection);
    }

    public void SetDirection(Vector2 direction)
    {
        Vector2 horizontalDir = new Vector2(direction.x, 0f);
        Direction = horizontalDir;
    }

    public void SetDirection(float xPos)
    {
        Direction = new Vector2(xPos, 0f);
    }

    public void Move()
    {
        Vector3 velocity = new Vector3(Direction.x, 0f, 0f) * Speed;
        velocity.y = Rigid.velocity.y;
        Rigid.velocity = velocity;
    }

    public abstract Quaternion GetRotation();

}
