using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IBattle
{
    private EntitySO _weaponSO;

    private const int PERCENT_EXCLUDE_MAX_VALUE = 101;

    private void OnTriggerEnter(Collider other)
    {
        HealthSO targetSO = null;
        IDamageable damageable = null;

        if (other.CompareTag("Player"))
        {
            targetSO = other.GetComponent<PlayerController>().StatHandler.Data;

            if (_weaponSO == null)
            {
                EnemyStatHandler statHandler = GetComponentInParent<EnemyController>().StatHandler;
                damageable = statHandler;
                _weaponSO = statHandler.Data;
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            targetSO = other.GetComponent<EnemyController>().StatHandler.Data;

            if (_weaponSO == null)
            {
                PlayerStatHandler statHandler = GetComponentInParent<PlayerController>().StatHandler;
                damageable = statHandler;
                _weaponSO = statHandler.Data;
            }
        }

        Attack(_weaponSO, targetSO, damageable);
    }

    public void Attack(EntitySO attacker, HealthSO target, IDamageable targetDamageable)
    {
        if (target.IsInvincible)
            return;

        int randomEvasionProbability = UnityEngine.Random.Range(0, PERCENT_EXCLUDE_MAX_VALUE);
        if (randomEvasionProbability < target.EvasionProbability)
            return;

        float attackDamage = attacker.Attack;

        int randomCriticalProbability = UnityEngine.Random.Range(0, PERCENT_EXCLUDE_MAX_VALUE);
        if (randomCriticalProbability < attacker.CriticalProbability)
            attackDamage += attacker.Attack * attacker.CriticalMod;

        targetDamageable.Damaged(attackDamage);
    }
}
