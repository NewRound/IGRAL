using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerStateBase : IState
{
    [Header("Speed")]
    protected float speedRatio;
    private SpeedCalculator _speedCalculator;
    private float _speed;

    [Header("Rotation")]
    private RotationCalculator _rotationCalculator;

    [Header("Movement")]
    private Movement _movement;
    private Transform _playerTrans;
    private Rigidbody _rigid;
    private Vector2 _direction;
    private Vector3 _preDirection;

    [Header("GroundCheck")]
    private GroundCheck _groundCheck;
    public bool IsGrounded { get; private set; }

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
        _rigid = playerController.Rigidbody;
        _playerTrans = playerController.transform;
        _preDirection = _playerTrans.forward;
        animationsData = playerController.AnimationData;

        _groundCheck = playerController.GroundCheck;

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
        IsGrounded = _groundCheck.CheckGround();
    }

    public virtual void OnDead()
    {
        inputActions.Player.Move.started -= playerController.OnMove;
        inputActions.Player.Move.canceled -= playerController.OnMove;
        playerController.MoveAction -= SetDirection;
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
        _rigid.velocity = new Vector3(_direction.x, _rigid.velocity.y, 0f) * _speed;
    }

    private void Look()
    {
        if (_direction == Vector2.zero)
        {
            _playerTrans.rotation = Quaternion.LookRotation(_preDirection);
            return;
        }

        _preDirection = _direction.x * Vector3.right;

        float newAngle = _rotationCalculator.CalculateRotation(_playerTrans.rotation.eulerAngles.y, _preDirection);

        _playerTrans.rotation = Quaternion.Euler(0f, newAngle, 0f);
    }
}