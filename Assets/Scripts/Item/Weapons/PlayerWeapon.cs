using UnityEngine;

public class PlayerWeapon : Weapon
{
    private PlayerSO _playerSO;

    public void Init(PlayerSO playerSO)
    {
        _playerSO = playerSO;
        weaponTag = Tag.Player.ToString();
    }

    public void UpdateStat(PlayerSO playerSO)
    {
        _playerSO = playerSO;
    }

    private void OnTriggerEnter(Collider other)
    {
        HealthSO targetSO = null;
        IDamageable damageable = null;

        if (other.CompareTag(weaponTag))
        {
            EnemyStatHandler statHandler = other.GetComponentInParent<EnemyController>().StatHandler;
            targetSO = statHandler.Data;
            damageable = statHandler;

            Attack(_playerSO, targetSO, damageable);
        }
    }
}