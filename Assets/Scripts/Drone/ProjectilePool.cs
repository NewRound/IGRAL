using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : CustomSingleton<ProjectilePool>
{
    [SerializeField] private int _projectileCount;
    [SerializeField] private DroneProjectile _projectile;    
    [SerializeField] private List<DroneProjectile> _projectiles;

    private void Awake()
    {
        for (int i = 0;i < _projectileCount; i++)
        {
            DroneProjectile projectile = Instantiate(_projectile, transform);
            _projectiles.Add(projectile);
            _projectiles[i].gameObject.SetActive(false);
        }
    }

    public DroneProjectile GetProjectile()
    {
        DroneProjectile selectProjectile = null;

        foreach (DroneProjectile projectile in _projectiles)
        {
            // ��Ȱ��ȭ�� �Ѿ��� ã�Ƽ� Ȱ��ȭ
            if (!projectile.gameObject.activeSelf)
            {
                selectProjectile = projectile;
                selectProjectile.gameObject.SetActive(true);
                Debug.Log("����ü Ȱ��ȭ");
                break;
            }
        }

        // ��Ȱ��ȭ�� �Ѿ��� ������ ����
        if (!selectProjectile)
        {
            Debug.Log("����ü ����");
            DroneProjectile projectile = Instantiate(_projectile, transform);
            selectProjectile = projectile.GetComponent<DroneProjectile>();
            _projectiles.Add(selectProjectile);
        }

        return selectProjectile;
    }
}
