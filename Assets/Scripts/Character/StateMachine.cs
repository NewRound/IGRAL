using UnityEngine;

public abstract class StateMachine
{
    public IState CurrentState { get; protected set; }

    [field: Header("Direction")]
    public Vector3 PreDirection { get; private set; }
    public Vector3 Direction { get; private set; }


    [field: Header("Speed")]
    public SpeedCalculator SpeedCalculator { get; protected set; }
    public float Speed { get; protected set; }
    public float SpeedRatio { get => speedRatio; protected set => speedRatio = value; }
    protected float speedRatio;
    protected float speedMin;
    protected float speedMax;

    [field: Header("Rotation")]
    public RotationCalculator RotationCalculator { get; protected set; }
    public Transform ModelTrans { get; protected set; }

    public bool IsDead { get; private set; }
    public bool IsAttacking { get; private set; }
    public Rigidbody Rigid;

    public abstract void Init();

    public void ChangeState(IState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
    }

    public virtual void Update()
    {
        CurrentState.UpdateState();
    }

    public virtual void PhysicsUpdate()
    {
        CurrentState.PhysicsUpdateState();
    }

    public virtual void Look()
    {
        SetPreDirection(Direction.x * Vector3.right);

        ModelTrans.rotation = GetRotation();
    }

    public virtual void UpdateSpeed()
    {
        Speed = SpeedCalculator.CalculateSpeed(speedMin, speedMax, out speedRatio, Direction == Vector3.zero);
    }

    public void SetPreDirection(Vector3 direction)
    {
        PreDirection = direction;
    }

    public void LookPreDirectionRightAway()
    {
        ModelTrans.rotation = Quaternion.LookRotation(PreDirection);
    }

    public void SetDirection(Vector3 direction)
    {
        Direction = direction;
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

    public virtual void Move()
    {
        Vector3 velocity = Direction * Speed;
        velocity.y = Rigid.velocity.y;
        Rigid.velocity = velocity;
    }

    public void SetDead(bool isDead)
    {
        IsDead = isDead;
    }

    public void SetIsAttacking(bool isAttacking)
    {
        IsAttacking = isAttacking;
    }

    public abstract Quaternion GetRotation();

    public void Knockback(Vector3 forward, float knockbackPower)
    {
        if (IsDead)
            return;

        Rigid.AddForce(forward * knockbackPower, ForceMode.Impulse);
    }
}
