using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    [Header("# Attack Info")]    
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackDelay;
    private float _attackTimer;

    [Header("# Projectile")]
    [SerializeField] private Transform _projectilePool;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private List<GameObject> _projectiles;

    [Header("# RayCast")]
    [SerializeField] private float rayAngle;
    [SerializeField] private int rayCount;
    [SerializeField] private LayerMask _enemyLayer;

    private void OnEnable()
    {
        // Ȱ��ȭ �� �� ����Ʈ �ʱ�ȭ
        _projectiles = new List<GameObject>();
    }

    public void ActiveDrone()
    {
        // Ȱ��ȭ
        gameObject.SetActive(true);
    }

    public void InActiveDrone()
    {
        // ��Ȱ��ȭ
        gameObject.SetActive(false);
    }

    private void Update()
    {
        _attackTimer += Time.deltaTime;

        if (_attackTimer > _attackDelay)
        {
            _attackTimer = 0;

            for (int i = 0; i <= rayCount; i++)
            {
                float angle = (float)i / rayCount * rayAngle - rayAngle / 2.0f;
                Vector3 rayDirection = Quaternion.Euler(0, 0, angle) * transform.forward;

                Debug.DrawRay(transform.position, rayDirection * _attackRange, Color.red);

                if (Physics.Raycast(transform.position, rayDirection, _attackRange, _enemyLayer))
                {
                    Debug.Log("Target In Range");                    
                    OnFire();
                    break;
                }

                else
                { 
                    Debug.Log("There is no Target");
                }
            }
        }
    }

    // �Ѿ��� �߻��ϴ� �޼���
    private void OnFire()
    {
        Debug.Log("OnFire");
        GameObject projectile = GetProjectile();

        projectile.transform.position = transform.position;

    }

    private GameObject GetProjectile()
    {
        GameObject selectProjectile = null;

        foreach (GameObject projectile in _projectiles)
        {
            // ����Ʈ�� ��Ȱ��ȭ�� �Ѿ��� ������ Ȱ��ȭ
            if(!projectile.activeSelf) 
            {
                selectProjectile = projectile;
                selectProjectile.SetActive(true);
                Debug.Log("����ü Ȱ��ȭ");
                break;
            }
        }

        // ����Ʈ�� �Ѿ��� ������ ����
        if (!selectProjectile) 
        {
            Debug.Log("����ü ����");
            selectProjectile = Instantiate(_projectile, _projectilePool);
            _projectiles.Add(selectProjectile);
        }

        return selectProjectile;
    }
}
