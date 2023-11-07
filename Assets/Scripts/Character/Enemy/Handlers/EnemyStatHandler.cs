using UnityEngine;

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

    public void Recovery(float damage)
    {
        Data.Health += damage;
        Debug.Log(damage);
    }
}
