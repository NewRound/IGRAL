using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : CustomSingleton<ProjectilePool>
{
    [SerializeField] private int _projectileCount;
    [SerializeField] private DroneBullet _projectile;    
    [SerializeField] private List<DroneBullet> _projectiles;

    private void Awake()
    {
        for (int i = 0;i < _projectileCount; i++)
        {
            DroneBullet projectile = Instantiate(_projectile, transform);
            _projectiles.Add(projectile);
            _projectiles[i].gameObject.SetActive(false);
        }
    }

    public DroneBullet GetProjectile()
    {
        DroneBullet selectProjectile = null;

        foreach (DroneBullet projectile in _projectiles)
        {
            // 비활성화된 총알을 찾아서 활성화
            if (!projectile.gameObject.activeSelf)
            {
                selectProjectile = projectile;
                selectProjectile.gameObject.SetActive(true);
                break;
            }
        }

        // 비활성화된 총알이 없으면 생성
        if (!selectProjectile)
        {
            DroneBullet projectile = Instantiate(_projectile, transform);
            selectProjectile = projectile.GetComponent<DroneBullet>();
            _projectiles.Add(selectProjectile);
        }

        return selectProjectile;
    }
}
