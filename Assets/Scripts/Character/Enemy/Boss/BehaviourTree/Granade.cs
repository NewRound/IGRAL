using DG.Tweening;
using GlobalEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : Weapon
{
    [SerializeField] private ParticleSystem[] explosionParticles;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private float damage = 15f;
    [SerializeField] private float explosionDelayTime = 2f;
    [SerializeField] private float explosionTime = 2f;
    [SerializeField] private float explosionRange = 2f;

    [SerializeField] private float rotationSpeed = 90f;

    private float _currentElapsedTime;
    private float _halfExplosiondelayTime;

    private Dictionary<float, WaitForSeconds> _explosionDict = new Dictionary<float, WaitForSeconds>();

    private Material _material;

    private Vector3 _initPos;
    private Vector3 _randomPos;
    private Transform _target;

    private void Awake()
    {
        _material = meshRenderer.material;
    }

    private void OnDisable()
    {
        InitPos();
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (_currentElapsedTime <= explosionDelayTime)
        {
            _currentElapsedTime += Time.deltaTime;
            _currentElapsedTime = _currentElapsedTime > explosionDelayTime ? explosionDelayTime : _currentElapsedTime;
        }

        Move();
        Rotate();
    }

    private void Move()
    {
        float ratio = _currentElapsedTime / explosionDelayTime;
        Vector3 firstLerp = Vector3.Lerp(_initPos, _randomPos, ratio);
        Vector3 secondLerp = Vector3.Lerp(_randomPos, _target.position, ratio);
        Vector3 finalLerp = Vector3.Lerp(firstLerp, secondLerp, ratio);

        transform.position = finalLerp;
    }

    private void Rotate()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed));
    }

    public void ThrowGranade()
    {
        if (_target == null)
        {
            Init();
        }

        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        _material.DOColor(Color.yellow, _halfExplosiondelayTime);
        yield return _explosionDict[_halfExplosiondelayTime];

        _material.DOColor(Color.red, _halfExplosiondelayTime);
        yield return _explosionDict[_halfExplosiondelayTime];

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRange, 1 << LayerMask.NameToLayer(Tag.Player.ToString()));
        if (colliders.Length > 0)
        {
            PlayerStatHandler statHandler = colliders[0].GetComponent<PlayerController>().StatHandler;
            Attack(damage, statHandler.Data, statHandler);
        }

        meshRenderer.gameObject.SetActive(false);

        foreach (ParticleSystem particleSystem in explosionParticles) 
            particleSystem.Play();

        yield return _explosionDict[explosionTime];

        _material.color = Color.white;
        gameObject.SetActive(false);
    }

    private void Init()
    {
        _initPos = transform.position;
        _target = GameManager.Instance.PlayerTransform;
        _halfExplosiondelayTime = GlobalValues.HALF * explosionDelayTime;
        _explosionDict.Add(_halfExplosiondelayTime, CoroutineRef.GetWaitForSeconds(_halfExplosiondelayTime));
        _explosionDict.Add(explosionTime, CoroutineRef.GetWaitForSeconds(explosionTime));

        InitPos();
    }

    private void InitPos()
    {
        if (_target != null)
        {
            Vector3 sphereRandomPos = Random.insideUnitSphere * Vector3.Distance(_initPos, _target.position) * GlobalValues.HALF;
            Vector3 canonicalPos = _initPos.x > _target.position.x ? _initPos - _target.position : _target.position - _initPos;
            _randomPos = canonicalPos + sphereRandomPos;
            _randomPos.z = 0f;
        }
    }
}