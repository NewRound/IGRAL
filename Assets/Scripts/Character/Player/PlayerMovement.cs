using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Accelerate")]
    [SerializeField] private float acceleratingTime = 10f;
    private SpeedCalculator _speedCalculator;
    private float _speed;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float minAbsAngle = 90f;
    [SerializeField] private float maxAbsAngle = 270f;
    private Vector3 _preDirection;
    private RotationCalculator _rotationCalculator;

    private Vector2 _direction;
    private PlayerController _controller;


    private Rigidbody _rigid;


    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _rigid = GetComponent<Rigidbody>();
        _speedCalculator = new SpeedCalculator(acceleratingTime);
        _rotationCalculator = new RotationCalculator(rotationSpeed, minAbsAngle, maxAbsAngle);
    }

    private void OnEnable()
    {
        _controller.MoveAction += SetDirection;
    }

    private void Start()
    {
        _preDirection = transform.forward;
    }

    private void OnDisable()
    {
        _controller.MoveAction -= SetDirection;
    }

    private void Update()
    {
        UpdateSpeed();
        Look();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    private void UpdateSpeed()
    {
        _speed = _speedCalculator.CalculateSpeed(
            _controller.StatHandler.Data.SpeedMin,
            _controller.StatHandler.Data.SpeedMax,
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
            transform.rotation = Quaternion.LookRotation(_preDirection);
            return;
        }

        _preDirection = _direction.x * Vector3.right;

        float newAngle = _rotationCalculator.CalculateRotation(transform.rotation.eulerAngles.y, _preDirection);
        
        transform.rotation = Quaternion.Euler(0f, newAngle, 0f);
    }
}
