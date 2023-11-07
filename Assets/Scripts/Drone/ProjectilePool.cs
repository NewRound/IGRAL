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
            // 비활성화된 총알을 찾아서 활성화
            if (!projectile.gameObject.activeSelf)
            {
                selectProjectile = projectile;
                selectProjectile.gameObject.SetActive(true);
                Debug.Log("투사체 활성화");
                break;
            }
        }

        // 비활성화된 총알이 없으면 생성
        if (!selectProjectile)
        {
            Debug.Log("투사체 생성");
            DroneProjectile projectile = Instantiate(_projectile, transform);
            selectProjectile = projectile.GetComponent<DroneProjectile>();
            _projectiles.Add(selectProjectile);
        }

        return selectProjectile;
    }
}
