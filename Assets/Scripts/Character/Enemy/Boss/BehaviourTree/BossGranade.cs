using DG.Tweening;
using GlobalEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGranade : Weapon
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
    private Vector3 _lastPos;
    private Transform _target;
    private GameObject _modelObject;

    private bool _isMovable;

    private void Awake()
    {
        _material = meshRenderer.material;
        _modelObject = meshRenderer.gameObject;
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (!_isMovable)
            return;

        CalculateExplosionTime();
        Move();
        Rotate();
    }

    public void ThrowGranade()
    {
        if (_target == null)
        {
            Init();
        }

        _isMovable = true;

        StartCoroutine(Explode());
    }

    public void InitPos()
    {
        if (_target != null)
        {
            _initPos = transform.position;
            _lastPos = _target.position;
            Vector3 sphereRandomPos = Random.insideUnitSphere * Vector3.Distance(_initPos, _target.position) * GlobalValues.HALF;
            Vector3 canonicalPos = (_initPos + _target.position) * GlobalValues.HALF;
            _randomPos = canonicalPos + sphereRandomPos;
            _randomPos.y = _randomPos.y < 0 ? 0 : _randomPos.y;
            _randomPos.z = 0f;
        }
    }

    public void ActivateModel()
    {
        _modelObject.SetActive(true);

    }

    public void DeActivateModel()
    {
        _modelObject.SetActive(false);
    }

    private void CalculateExplosionTime()
    {
        if (_currentElapsedTime <= explosionDelayTime)
        {
            _currentElapsedTime += Time.deltaTime;
            _currentElapsedTime = _currentElapsedTime > explosionDelayTime ? explosionDelayTime : _currentElapsedTime;
        }
    }

    private void Move()
    {
        float ratio = _currentElapsedTime / explosionDelayTime;
        Vector3 firstLerp = Vector3.Lerp(_initPos, _randomPos, ratio);
        Vector3 secondLerp = Vector3.Lerp(_randomPos, _lastPos, ratio);
        Vector3 finalLerp = Vector3.Lerp(firstLerp, secondLerp, ratio);

        transform.position = finalLerp;
    }

    private void Rotate()
    {
        _modelObject.transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
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

        DeActivateModel();

        foreach (ParticleSystem particleSystem in explosionParticles) 
            particleSystem.Play();

        yield return _explosionDict[explosionTime];

        ResetValues();

        _material.color = Color.white;
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

    

    private void ResetValues()
    {
        _isMovable = false;
        _currentElapsedTime = 0f;
    }
}