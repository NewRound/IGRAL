using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Drone : MonoBehaviour
{
    [Header("# Attack Info")]
    [SerializeField] private float _attackDamage;    
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackDelay;
    private float _attackTimer;

    [Header("# Projectile")]
    [SerializeField] private Transform _projectilePool;
    [SerializeField] private GameObject _projectile;
    private List<GameObject> _projectiles;

    [Header("# RayCast")]
    float rayAngle = 60f;
    int rayCount = 12;
    [SerializeField] private LayerMask _enemyLayer;
    private bool _targetInRange;

    private void OnEnable()
    {
        // 활성화 될 때 리스트 초기화
        _projectiles = new List<GameObject>();
    }

    public void ActiveDrone()
    {
        // 활성화
        gameObject.SetActive(true);
    }

    public void InActiveDrone()
    {
        // 비활성화
        gameObject.SetActive(false);
    }

    private void Update()
    {
        _attackTimer += Time.deltaTime;       

        for (int i = 0; i <= rayCount; i++)
        {
            float angle = (float)i / rayCount * rayAngle - rayAngle / 2.0f;
            Vector3 rayDirection = Quaternion.Euler(0, 0, angle) * transform.forward;

            Debug.DrawRay(transform.position, rayDirection * _attackRange, Color.red);

            if (Physics.Raycast(transform.position,rayDirection,_attackRange,_enemyLayer))
            {
                Debug.Log("Target In Range");
                _targetInRange = true;     
                break;                
            }
            else
            {
                Debug.Log("There is no Target");
                _targetInRange = false;
            }
        }
        
        if (_targetInRange && _attackTimer >= _attackDelay)
        {
            // 처음 사거리에 들어오면 공격
            // 딜레이만큼 공격 안하기

            _attackTimer = 0;

            Debug.Log("OnFire");
            OnFire();
        }
    }

    // 총알을 발사하는 메서드
    private void OnFire()
    {
        GameObject projectile = GetProjectile();

        projectile.transform.position = transform.position;

    }

    private GameObject GetProjectile()
    {
        GameObject selectProjectile = null;

        foreach (GameObject projectile in _projectiles)
        {
            // 리스트에 비활성화된 총알이 있으면 활성화
            if(!projectile.activeSelf) 
            {
                selectProjectile = projectile;
                selectProjectile.SetActive(true);
                Debug.Log("투사체 활성화");
                break;
            }
        }

        // 리스트에 총알이 없으면 생성
        if (!selectProjectile) 
        {
            Debug.Log("투사체 생성");
            selectProjectile = Instantiate(_projectile, _projectilePool);
            _projectiles.Add(selectProjectile);
        }

        return selectProjectile;
    }
}
