public class EnemyStatHandler : IDamageable
{
    public EnemySO Data { get; private set; }

    public EnemyStatHandler(EnemySO data)
    {
        Data = data;
    }

    public void Damaged(float damage)
    {
        Data.Health -= damage;
    }
}
