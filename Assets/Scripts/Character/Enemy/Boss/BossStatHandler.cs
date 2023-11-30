using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatHandler : EnemyStatHandler
{
    private UIBossCondition _bossCondition;

    public BossStatHandler(EnemySO data, BossBehaviourTree bossBehaviourTree) : base(data, null, null, bossBehaviourTree.transform)
    {
        _bossCondition = bossBehaviourTree.UIBossCondition;
        DamagedAction += UpdateHealth;
    }

    public void Init()
    {
        UpdateMaxHp();
        UpdateHealth();
    }

    private void UpdateMaxHp()
    {
        _bossCondition.SetMaxHP(Data.MaxHealth);
    }

    private void UpdateHealth()
    {
        _bossCondition.DisplayHP(Data.Health);
    }
}
