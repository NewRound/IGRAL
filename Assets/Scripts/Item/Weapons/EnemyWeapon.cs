using UnityEngine;

public class EnemyWeapon : Weapon
{
    private EnemySO _enemySO;
   
    public void Init(EnemySO enemySO)
    {
        _enemySO = enemySO;
        weaponTag = Tag.Enemy.ToString();
    }

    public void UpdateStat(EnemySO enemySO)
    {
        _enemySO = enemySO;
    }

    private void OnTriggerEnter(Collider other)
    {
        HealthSO targetSO = null;
        IDamageable damageable = null;

        if (other.CompareTag(weaponTag))
        {
            PlayerStatHandler statHandler = other.GetComponentInParent<PlayerController>().StatHandler;
            targetSO = statHandler.Data;
            damageable = statHandler;

            Attack(_enemySO, targetSO, damageable);
        }
    }
}