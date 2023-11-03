public interface IBattle
{
    void Attack(EntitySO attacker, HealthSO target, IDamageable damageable);
}