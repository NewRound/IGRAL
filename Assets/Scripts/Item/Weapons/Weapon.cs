using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tag
{
    Player,
    Enemy,
}

public class Weapon : MonoBehaviour, IBattle
{
    private const int PERCENT_EXCLUDE_MAX_VALUE = 101;

    protected string weaponTag;

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
