﻿using Cinemachine.Utility;
using GlobalEnums;
using System.Collections;
using UnityEngine;

public class Rocket : ExplosionWeapon
{
    [SerializeField] private GameObject crosshairPrefab;
    [SerializeField] private GameObject trail;

    private Transform _targetTrans;
    private Transform _bulletSpawnTrans;
    private Vector3 _initPos;
    private Vector3 _randomPos;
    private Vector3 _lastPos;
    private float _elapsedTime;
    private GameObject _crosshairInstant;

    private bool _isTimeOver;
    private bool _wasCollided;

    private void Awake()
    {
        Init();
    }

    protected void OnEnable()
    {
        Activate();
        InitPos();
        StartCoroutine(Explode());
    }


    private void Update()
    {
        if (_wasCollided || _isTimeOver)
            return;

        CalculateTime();
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();

        if (playerController != null)
        {
            if (_isTimeOver)
                return;

            _wasCollided = true;
            Attack(damage, playerController.StatHandler.Data, playerController.StatHandler);
            StartCoroutine(DestroySelf());
        }
    }

    public override void Activate()
    {
        base.Activate();
        _isTimeOver = false;
        _crosshairInstant.SetActive(true);
        trail.SetActive(true);
    }

    public override void DeActivate()
    {
        base.DeActivate();
        trail.SetActive(false);
    }

    public void SetSpawnTrans(Transform bulletSpawnTrans)
    {
        _bulletSpawnTrans = bulletSpawnTrans;
        InitPos();
    }

    protected override void ResetValues()
    {
        _elapsedTime = 0f;
        _crosshairInstant.SetActive(false);
        _wasCollided = false;
    }

    protected override IEnumerator DestroySelf()
    {
        _isTimeOver = true;

        yield return StartCoroutine(base.DestroySelf());
    }

    protected override IEnumerator Explode()
    {
        yield return StartCoroutine(ChangeColor());
        ApplyExplosionDamage();

        if (!_wasCollided)
            StartCoroutine(DestroySelf());
    }

    protected override void Init()
    {
        base.Init();
        if (_crosshairInstant == null)
            _crosshairInstant = Instantiate(crosshairPrefab, _lastPos, Quaternion.identity);
        InitPos();
    }

    private void InitPos()
    {
        if (_bulletSpawnTrans != null)
            _initPos = _bulletSpawnTrans.position;

        _targetTrans = GameManager.Instance.PlayerTransform;
        _lastPos = _targetTrans.position;
        _lastPos.y += yOffset;
        float radius = GlobalValues.HALF * Vector3.Distance(_initPos, _targetTrans.position);
        _randomPos = (_initPos + _lastPos) * GlobalValues.HALF + Random.insideUnitSphere * radius;
        _randomPos.y = _randomPos.y < _lastPos.y ? _lastPos.y + radius : _randomPos.y;
        _crosshairInstant.transform.position = _lastPos;
    }

    private void Move()
    {
        float ratio = _elapsedTime / explosionDelayTime;

        Vector3 firstLerp = Vector3.Lerp(_initPos, _randomPos, ratio);
        Vector3 secondLerp = Vector3.Lerp(_randomPos, _lastPos, ratio);
        Vector3 finalLerp = Vector3.Lerp(firstLerp, secondLerp, ratio);

        transform.position = finalLerp;

        Vector3 direction = _targetTrans.position - finalLerp;
        Look(direction);
    }

    private void CalculateTime()
    {
        _elapsedTime += Time.deltaTime;
        _elapsedTime = _elapsedTime > explosionDelayTime ? explosionDelayTime : _elapsedTime;
    }

    private void Look(Vector3 direction)
    {
        Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, direction);
        transform.rotation = rotation;
    }

    
}