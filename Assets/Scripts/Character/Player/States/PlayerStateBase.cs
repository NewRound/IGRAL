using UnityEngine;

public abstract class PlayerStateBase : IState
{
    [Header("Speed")]
    protected float speedRatio;
    private SpeedCalculator _speedCalculator;
    private float _speed;

    [Header("Rotation")]
    private RotationCalculator _rotationCalculator;

    [Header("Movement")]
    protected Rigidbody rigid;
    protected Transform playerTrans;
    private Movement _movement;
    private Vector2 _direction;
    private Vector3 _preDirection;

    [Header("Player")]
    protected StateMachine stateMachine;
    protected PlayerController playerController;
    protected PlayerAnimationsData animationsData;

    [Header("Input")]
    protected PlayerInputAction inputActions;

    public PlayerStateBase(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;

        playerController = stateMachine.PlayerController;

        _movement = playerController.Movement;
        rigid = playerController.Rigidbody;
        playerTrans = playerController.transform;
        _preDirection = playerTrans.forward;
        animationsData = playerController.AnimationData;

        InitInputActions();

        _speedCalculator = new SpeedCalculator(_movement.AcceleratingTime);
        _rotationCalculator = new RotationCalculator(_movement.RotationSpeed, _movement.MinAbsAngle, _movement.MaxAbsAngle);
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    public abstract void Enter();

    public abstract void Exit();

    public virtual void UpdateState()
    {
        UpdateSpeed();
        Look();
    }

    public virtual void PhysicsUpdateState()
    {
        Move();
    }

    public virtual void OnDead()
    {
        playerController.MoveAction -= SetDirection;
    }

    protected void PlayAnimation(int animationParameterHash, bool isPlaying)
    {
        playerController.Animator.SetBool(animationParameterHash, isPlaying);
    }

    protected void PlayAnimation(int animationParameterHash, int integerValue)
    {
        playerController.Animator.SetInteger(animationParameterHash, integerValue);
    }

    protected void PlayAnimation(int animationParameterHash, float floatValue)
    {
        playerController.Animator.SetFloat(animationParameterHash, floatValue);
    }

    private void UpdateSpeed()
    {
        _speed = _speedCalculator.CalculateSpeed(
            playerController.StatHandler.Data.SpeedMin,
            playerController.StatHandler.Data.SpeedMax,
            out speedRatio,
            _direction == Vector2.zero);
    }

    private void InitInputActions()
    {
        inputActions = playerController.InputActions;
        
        playerController.MoveAction += SetDirection;
    }

    private void Move()
    {
        Vector3 velocity = new Vector3(_direction.x, 0f, 0f) * _speed;
        velocity.y = rigid.velocity.y;
        rigid.velocity = velocity;
    }

    private void Look()
    {
        if (_direction == Vector2.zero)
        {
            playerTrans.rotation = Quaternion.LookRotation(_preDirection);
            return;
        }

        _preDirection = _direction.x * Vector3.right;

        float newAngle = _rotationCalculator.CalculateRotation(playerTrans.rotation.eulerAngles.y, _preDirection);

        playerTrans.rotation = Quaternion.Euler(0f, newAngle, 0f);
    }

    
}
