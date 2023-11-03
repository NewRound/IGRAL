public class PlayerStatHandler : IDamageable
{
    public PlayerSO Data { get; private set; }

    public PlayerStatHandler(PlayerSO data)
    {
        Data = data;
    }

    public void Damaged(float damage)
    {
        Data.Health -= damage;
    }

}
