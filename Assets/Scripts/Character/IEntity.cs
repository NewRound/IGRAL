public interface IEntity : IDamageable
{
    void Attack(EntityData attacker, HealthData target);
}