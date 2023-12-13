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
    private Vector3 yOffset = new Vector3(0f, 1.6f, 0f);

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

            if (UIEnemyHealth != null && Data.Health <= 0)
            {
                UIEnemyHealth.gameObject.SetActive(false);
                DieAction?.Invoke();
            }

            if (UIEnemyHealth != null)
                UIEnemyHealth.DisplayEnemyHealth(Data.Health, Data.MaxHealth);

            GameObject damagedTxt = ObjectPoolingManager.Instance.GetGameObject(ObjectPoolType.EnemyDamagedTxt);
            damagedTxt.GetComponent<DamagedTxt>().SetDamage(damage);
            damagedTxt.transform.position = curTransform.position + yOffset;
            

            EffectManager.Instance.ShowEffect(curTransform.position + yOffset, EffectType.Damaged);
            AudioManager.Instance.PlaySFX(SFXType.Damaged1);
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
