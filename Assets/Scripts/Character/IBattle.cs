public interface IBattle
{
    void Attack(EntitySO attacker, HealthSO target, IDamageable damageable, float attackMod = 1f);
}