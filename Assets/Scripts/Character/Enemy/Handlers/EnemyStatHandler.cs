using System;
using UnityEngine;

public class EnemyStatHandler : IDamageable
{
    public EnemySO Data { get; private set; }
    private UIEnemyHealth UIEnemyHealth;
    private GameObject EnemyArmor;

    public event Action DamagedAction;
    public event Action DieAction;

    private Transform curTransform;

    public EnemyStatHandler(EnemySO data, UIEnemyHealth uIEnemyHealth, GameObject enemyArmor, Transform baseTransform)
    {
        Data = data;
        UIEnemyHealth = uIEnemyHealth;
        EnemyArmor = enemyArmor;
        curTransform = baseTransform;
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

            GameObject damagedTxt = ObjectPoolingManager.Instance.GetGameObject(ObjectPoolType.EnemyDamagedTxt);
            damagedTxt.transform.position = curTransform.position + new Vector3(0f, 1.8f, 0f);
            damagedTxt.GetComponent<DamagedTxt>()._damage = damage;
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
