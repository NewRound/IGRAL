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
    [SerializeField] private float turningSpeed = 10f;
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
        // TODO : 주석 부분은 회전을 통일시키기 위해 테스트 해본 것 (아직 미완성)

        if (_direction == Vector2.zero)
        {
            //transform.rotation = Quaternion.LookRotation(_preDirection);
            return;
        }

        _preDirection = (Vector3.right * _direction.x).normalized;

        Quaternion rotation = Quaternion.Slerp(
            transform.rotation, 
            Quaternion.LookRotation(_preDirection), 
            turningSpeed * Time.deltaTime);
        
        Debug.Log($"rotation : {rotation.eulerAngles}");

        //if (rotation.eulerAngles.y >= -90 && rotation.eulerAngles.y < 0)
        //{
        //    rotation = Quaternion.LookRotation(Vector3.left);
        //}
        //else if (rotation.eulerAngles.y > 0 && rotation.eulerAngles.y <= 90)
        //{
        //    rotation = Quaternion.LookRotation(Vector3.right);
        //}

        transform.rotation = rotation;
    }
}
