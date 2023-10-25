using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Accelerate")]
    [SerializeField] private float acceleratingTime = 10f;
    private float _speed;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float minAbsAngle = 90f;
    [SerializeField] private float maxAbsAngle = 270f;
    private Vector3 _preDirection;

    private Vector2 _direction;
    private PlayerController _controller;

    private SpeedCalculator _speedCalculator;

    private Rigidbody _rigid;


    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _rigid = GetComponent<Rigidbody>();
        _speedCalculator = new SpeedCalculator(acceleratingTime);
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

        // LookRotation로는 제한을 두기 어렵다고 생각하여 오일러 각도로 변환하여 계산하는 것이 나아보임

        float currentAngle = transform.rotation.eulerAngles.y;
        float targetAngle = Vector3.SignedAngle(Vector3.forward, _preDirection, Vector3.up);

        float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);

        newAngle = newAngle >= 0 ? 
            Mathf.Clamp(Mathf.Abs(newAngle), minAbsAngle, maxAbsAngle) : 
            -Mathf.Clamp(Mathf.Abs(newAngle), minAbsAngle, maxAbsAngle);

        transform.rotation = Quaternion.Euler(0f, newAngle, 0f);
    }
}
