using System.Collections;
using UnityEngine;

public class Rocket : BulletBase
{
    [SerializeField] private float arrvingDuration = 1f;
    private Transform _targetTrans;
    private Vector3 _initPos;
    private Vector3 _randomPos;
    private float _elapsedTime;

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    private void Update()
    {
        Move();
    }

    private void Init()
    {
        _targetTrans = GameManager.Instance.PlayerTransform;
        _initPos = transform.position;
        float radius = GlobalValues.HALF * Vector3.Distance(_initPos, _targetTrans.position);
        _randomPos = (_initPos + _targetTrans.position) * GlobalValues.HALF + Random.insideUnitSphere * radius;

        Vector3 direction = _targetTrans.position - _initPos;
        Look(direction);
    }

    private void Move()
    {
        _elapsedTime += Time.deltaTime;
        _elapsedTime = _elapsedTime > arrvingDuration ? arrvingDuration : _elapsedTime;

        Vector3 firstLerp = Vector3.Lerp(_initPos, _randomPos, _elapsedTime);
        Vector3 secondLerp = Vector3.Lerp(_randomPos, _targetTrans.position, _elapsedTime);
        Vector3 finalLerp = Vector3.Lerp(firstLerp, secondLerp, _elapsedTime);

        transform.position = finalLerp;

        Vector3 direction = _targetTrans.position - finalLerp;
        Look(direction);
    }

    private void Look(Vector3 direction)
    {
        Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, direction);
        transform.rotation = rotation;
    }
}