using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateBase : IState
{
    private SpeedCalculator _speedCalculator;
    private float _speed;

    private RotationCalculator _rotationCalculator;
    private Vector3 _preDirection;

    private Vector2 _direction;

    private Movement _movement;
    private PlayerController _controller;
    private Transform _playerTrans;
    private Rigidbody _rigid;

    public PlayerStateBase(PlayerController playerController, Movement movement)
    {
        _controller = playerController;

        _movement = movement;
        _rigid = _controller.Rigidbody;
        _playerTrans = _controller.transform;
        _preDirection = _playerTrans.forward;

        PlayerInputAction actions = _controller.InputActions;
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
            _controller.StatHandler.Data.SpeedMin,
            _controller.StatHandler.Data.SpeedMax,
            _direction == Vector2.zero);
        Debug.Log(_speed);
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

    public void ChangeState()
    {
    }

    public void Enter()
    {
    }

    public void Exit()
    {
    }

    public void UpdateState()
    {
        UpdateSpeed();
        Look();
    }

    public void PhysicsUpdateState()
    {
        Move();
    }

    public void OnDead()
    {
        PlayerInputAction actions = _controller.InputActions;
        actions.Player.Move.started -= SetDirection;
        actions.Player.Move.canceled += SetDirection;
    }
}
