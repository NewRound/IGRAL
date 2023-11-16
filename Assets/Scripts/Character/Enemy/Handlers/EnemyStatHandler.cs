using System;
using UnityEngine;

public class EnemyStatHandler : IDamageable
{
    public EnemySO Data { get; private set; }
    private UIEnemyHealth UIEnemyHealth;
    private GameObject EnemyArmor;

    public event Action DamagedAction;
    public event Action DieAction;

    public EnemyStatHandler(EnemySO data, UIEnemyHealth uIEnemyHealth, GameObject enemyArmor)
    {
        Data = data;
        UIEnemyHealth = uIEnemyHealth;
        EnemyArmor = enemyArmor;
    }

    public void Damaged(float damage)
    {
        if(Data.Armor <= 0)
        {
            Data.Health -= damage;
            DamagedAction?.Invoke();

            if (Data.Health <= 0)
            {
                DieAction?.Invoke();
            }

            UIEnemyHealth.DisplayEnemyHealth(Data.Health, Data.MaxHealth);
        }
        else
        {
            if(GameManager.Instance.PlayerAppearanceController.mutantType == MutantType.Stone)
            {
                Data.Armor -= damage;
                DamagedAction?.Invoke();

                if (Data.Armor <= 0)
                {
                    EnemyArmor.SetActive(false);
                }
            }
            UIEnemyHealth.DisplayEnemyArmor(Data.Armor, Data.MaxArmor);
        }

    }

    public void Recovery(float damage)
    {
        Data.Health += damage;
    }
}
