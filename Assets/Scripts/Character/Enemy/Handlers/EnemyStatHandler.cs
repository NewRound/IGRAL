using System;
using System.Diagnostics;

public class EnemyStatHandler : IDamageable
{
    public EnemySO Data { get; private set; }
    private UIEnemyHealth UIEnemyHealth;

    public event Action DamagedAction;
    public event Action DieAction;

    public EnemyStatHandler(EnemySO data, UIEnemyHealth uIEnemyHealth)
    {
        Data = data;
        UIEnemyHealth = uIEnemyHealth;
    }

    public void Damaged(float damage)
    {
        Data.Health -= damage;
        DamagedAction?.Invoke();

        if (Data.Health <= 0)
        {
            DieAction?.Invoke();
        }

        UIEnemyHealth.DisplayEnemyHealth(Data.Health, Data.MaxHealth);
    }

    public void Recovery(float damage)
    {
        Data.Health += damage;
    }
}
