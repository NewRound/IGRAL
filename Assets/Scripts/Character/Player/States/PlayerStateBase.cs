using UnityEditor;
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


    [Header("Player")]
    protected StateMachine stateMachine;
    protected PlayerController playerController;
    protected PlayerAnimationsData animationsData;

    public PlayerStateBase(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;

        playerController = stateMachine.PlayerController;

        _movement = playerController.Movement;
        _rigid = playerController.Rigidbody;
        _playerTrans = playerController.transform;
        _preDirection = _playerTrans.forward;
        animationsData = playerController.AnimationData;

        PlayerInputAction actions = playerController.InputActions;
        actions.Player.Move.started += SetDirection;
        actions.Player.Move.canceled += SetDirection;

        _speedCalculator = new SpeedCalculator(_movement.acceleratingTime);
        _rotationCalculator = new RotationCalculator(_movement.rotationSpeed, _movement.minAbsAngle, _movement.maxAbsAngle);
    }

    public void SetDirection(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector2>();
    }

    private void UpdateSpeed()
    {
        _speed = _speedCalculator.CalculateSpeed(
            playerController.StatHandler.Data.SpeedMin,
            playerController.StatHandler.Data.SpeedMax,
            out speedRatio,
            _direction == Vector2.zero);
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

    private void Jump()
    {

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
        PlayerInputAction actions = playerController.InputActions;
        actions.Player.Move.started -= SetDirection;
        actions.Player.Move.canceled -= SetDirection;
    }
}
