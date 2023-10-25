using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Accelerate")]
    [SerializeField] private float acceleratingTime = 1f;

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
    }

    private void OnDisable()
    {
        _controller.MoveAction -= SetDirection;
    }

    void Update()
    {
        Move();
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    private void Move()
    {
        float speed = _speedCalculator.CalculateSpeed(
            _controller.StatHandler.Data.SpeedMin, 
            _controller.StatHandler.Data.SpeedMax,
            _direction == Vector2.zero);

        _rigid.velocity = new Vector3(_direction.x, _rigid.velocity.y, 0f) * speed;
    }
}
