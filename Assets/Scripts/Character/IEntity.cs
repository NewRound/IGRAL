public interface IEntity : IDamageable
{
    void Attack(EntitySO attacker, HealthSO target);
}