using System;
using UnityEngine;

public class EnemyStatHandler : IDamageable
{
    public EnemySO Data { get; private set; }

    public event Action DamagedAction;
    public event Action DieAction;

    public EnemyStatHandler(EnemySO data)
    {
        Data = data;
    }

    public void Damaged(float damage)
    {
        Data.Health -= damage;

        DamagedAction?.Invoke();

        if (Data.Health <= 0)
        {
            DieAction?.Invoke();
        }
    }

    public void Recovery(float damage)
    {
        Data.Health += damage;
        Debug.Log(damage);
    }
}
