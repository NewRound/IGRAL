using System.Collections;
using UnityEngine;

public class BossGranade : ExplosionWeapon
{
    private Vector3 _initPos;
    private Vector3 _randomPos;
    private Vector3 _lastPos;
    private Transform _target;

    private bool _isMovable;

    private void Awake()
    {
        modelObject = meshRenderer.gameObject;
    }

    private void Update()
    {
        if (!_isMovable)
            return;

        CalculateExplosionTime();
        Move();
        Rotate();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, explosionRange);
    }
#endif

    public void ThrowGranade()
    {
        if (_target == null)
            Init();

        InitPos();
        _isMovable = true;

        StartCoroutine(Explode());
    }

    public void InitPos()
    {
        if (_target != null)
        {
            _initPos = transform.position;
            _lastPos = _target.position;
            _lastPos.y += yOffset;
            float radius = Vector3.Distance(_initPos, _target.position) * GlobalValues.HALF;
            Vector3 sphereRandomPos = Random.insideUnitSphere * radius;
            Vector3 canonicalPos = (_initPos + _target.position) * GlobalValues.HALF;
            _randomPos = canonicalPos + sphereRandomPos;
            _randomPos.y = _randomPos.y < _target.position.y ? radius + yOffset : _randomPos.y;
            _randomPos.z = 0f;
        }
    }

    private void CalculateExplosionTime()
    {
        if (currentElapsedTime <= explosionDelayTime)
        {
            currentElapsedTime += Time.deltaTime;
            currentElapsedTime = currentElapsedTime > explosionDelayTime ? explosionDelayTime : currentElapsedTime;
        }
    }

    private void Move()
    {
        float ratio = currentElapsedTime / explosionDelayTime;
        Vector3 firstLerp = Vector3.Lerp(_initPos, _randomPos, ratio);
        Vector3 secondLerp = Vector3.Lerp(_randomPos, _lastPos, ratio);
        Vector3 finalLerp = Vector3.Lerp(firstLerp, secondLerp, ratio);

        transform.position = finalLerp;
    }

    private void Rotate()
    {
        modelObject.transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }

    protected override void Init()
    {
        base.Init();
        _target = GameManager.Instance.PlayerTransform;
    }

    protected override IEnumerator DestroySelf()
    {
        DeActivateModel();

        foreach (ParticleSystem particleSystem in explosionParticles)
            particleSystem.Play();

        yield return explosionDict[explosionTime];

        material.color = Color.white;
    }

    protected override void ResetValues()
    {
        _isMovable = false;
        currentElapsedTime = 0f;
    }
}