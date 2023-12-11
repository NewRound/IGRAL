using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatHandler : EnemyStatHandler
{
    private BossBehaviorTree _bossBehaviourTree;
    private UIBossCondition _bossCondition;

    public BossStatHandler(EnemySO data, BossBehaviorTree bossBehaviourTree) : base(data, null, null, bossBehaviourTree.transform)
    {
        _bossBehaviourTree = bossBehaviourTree;
        DamagedAction += UpdateHealth;
    }

    public void Init()
    {
        _bossCondition = _bossBehaviourTree.UIBossCondition;
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
