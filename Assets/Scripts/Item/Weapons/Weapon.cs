using UnityEngine;

public abstract class Weapon : MonoBehaviour, IBattle
{
    private const int PERCENT_EXCLUDE_MAX_VALUE = 101;

    protected string targetTag;

    public void Attack(EntitySO attacker, HealthSO target, IDamageable targetDamageable, float attackMod = 1f)
    {
        if (target.IsInvincible)
            return;

        int randomEvasionProbability = UnityEngine.Random.Range(0, PERCENT_EXCLUDE_MAX_VALUE);
        if (randomEvasionProbability < target.EvasionProbability)
            return;

        float attackDamage = attacker.Attack * attackMod;

        Debug.Log(attackDamage);

        int randomCriticalProbability = UnityEngine.Random.Range(0, PERCENT_EXCLUDE_MAX_VALUE);
        if (randomCriticalProbability < attacker.CriticalProbability)
            attackDamage += attacker.Attack * attacker.CriticalMod;

        targetDamageable.Damaged(attackDamage);
    }

    public void Attack(float damage, HealthSO target, IDamageable targetDamageable, float attackMod = 1f)
    {
        if (target.IsInvincible)
            return;

        int randomEvasionProbability = UnityEngine.Random.Range(0, PERCENT_EXCLUDE_MAX_VALUE);
        if (randomEvasionProbability < target.EvasionProbability)
            return;

        float attackDamage = damage * attackMod;

        targetDamageable.Damaged(attackDamage);
    }


    public void Attack(IInteract interactable)
    {
        interactable.Interact();
    }

    protected virtual void OnAttack()
    {

    }
}
