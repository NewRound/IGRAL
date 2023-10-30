using System;
using UnityEngine;

public class MovementDataHandler
{
    private Vector2 _direction;
    public Vector3 PreDirection { get; private set; }

    public Rigidbody Rigid;
    private Transform _playerTrans;

    [Header("Speed")]
    private SpeedCalculator _speedCalculator;
    private float _speed;
    private float _speedRatio;

    [Header("Rotation")]
    private RotationCalculator _rotationCalculator;

    [Header("Datas")]
    private PlayerStatHandler _playerStatHandler;
    private RollDataHandler _rollDataHandler;

    public MovementDataHandler(MovementData movementData, PlayerStatHandler playerStatHandler, RollDataHandler rollDataHandler, Rigidbody rigidbody, Transform playerTrans)
    {
        _speedCalculator = new SpeedCalculator(movementData.AcceleratingTime);
        _rotationCalculator = new RotationCalculator(movementData.RotationSpeed, movementData.MinAbsAngle, movementData.MaxAbsAngle);
        _playerStatHandler = playerStatHandler;
        _rollDataHandler = rollDataHandler;
        Rigid = rigidbody;
        _playerTrans = playerTrans;

        PreDirection = _playerTrans.forward;
    }

    public void UpdateSpeed()
    {
        _speed = _speedCalculator.CalculateSpeed(
            _playerStatHandler.Data.SpeedMin,
            _playerStatHandler.Data.SpeedMax,
            out _speedRatio,
            _direction == Vector2.zero);
    }

    public void Move()
    {
        Vector3 velocity = new Vector3(_direction.x, 0f, 0f) * _speed;
        velocity.y = Rigid.velocity.y;
        Rigid.velocity = velocity;
    }

    public void Look()
    {
        if (_rollDataHandler.IsRolling)
            return;

        if (_direction.x == 0)
        {
            _playerTrans.rotation = Quaternion.LookRotation(PreDirection);
            return;
        }

        PreDirection = _direction.x * Vector3.right;


        float newAngle = _rotationCalculator.CalculateRotation(_playerTrans.rotation.eulerAngles.y, PreDirection);

        _playerTrans.rotation = Quaternion.Euler(0f, newAngle, 0f);
    }

    public float GetSpeedRatio()
    {
        return _speedRatio;
    }

    internal void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }
}