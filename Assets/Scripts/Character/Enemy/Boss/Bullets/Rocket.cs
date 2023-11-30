using GlobalEnums;
using System.Collections;
using UnityEngine;

public class Rocket : Weapon
{
    [SerializeField] private GameObject crosshair;
    [SerializeField] private float arrvingDuration = 1f;
    [SerializeField] private float damage = 10f;

    private Transform _targetTrans;
    private Vector3 _initPos;
    private Vector3 _randomPos;
    private Vector3 _lastPos;
    private float _elapsedTime;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        CalculateTime();
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();

        if (playerController != null)
        {
            Attack(damage, playerController.StatHandler.Data, playerController.StatHandler);
            GameObject explosion = EffectManager.Instance.GetEffect(EffectType.ExplosionParticle);
            explosion.transform.position = transform.position;
            Destroy(gameObject);
        }
    }

    private void Init()
    {
        _initPos = transform.position;
        _targetTrans = GameManager.Instance.PlayerTransform;
        float radius = GlobalValues.HALF * Vector3.Distance(_initPos, _targetTrans.position);
        _randomPos = (_initPos + _targetTrans.position) * GlobalValues.HALF + Random.insideUnitSphere * radius;
        _randomPos.y = _randomPos.y < 0 ? 0 : _randomPos.y;

        

        Vector3 direction = _targetTrans.position - _initPos;
        Look(direction);
    }

    private void Move()
    {
        Vector3 firstLerp = Vector3.Lerp(_initPos, _randomPos, _elapsedTime);
        Vector3 secondLerp = Vector3.Lerp(_randomPos, _targetTrans.position, _elapsedTime);
        Vector3 finalLerp = Vector3.Lerp(firstLerp, secondLerp, _elapsedTime);

        transform.position = finalLerp;

        Vector3 direction = _targetTrans.position - finalLerp;
        Look(direction);
    }

    private void CalculateTime()
    {
        _elapsedTime += Time.deltaTime;
        _elapsedTime = _elapsedTime > arrvingDuration ? arrvingDuration : _elapsedTime;
    }

    private void Look(Vector3 direction)
    {
        Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, direction);
        transform.rotation = rotation;
    }
}