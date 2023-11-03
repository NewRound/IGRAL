using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipManager : CustomSingleton<EquipManager>
{
    [SerializeField] private GameObject backWeaponPos;
    [SerializeField] private GameObject HandWeaponPos;

    [SerializeField] private EquipMentStats baseStats;
    public EquipMentStats CurrentStats { get; private set; }
    public List<EquipMentStats> statsModifiers = new List<EquipMentStats>();

    private void Awake()
    {        
        UpdateEquipmentStats();
    }

    public void AddStatModifier(EquipMentStats statModifier)
    {
        statsModifiers.Add(statModifier);
        UpdateEquipmentStats();
    }

    public void RemoveStatModifier(EquipMentStats statModifier)
    {
        statsModifiers.Remove(statModifier);
        UpdateEquipmentStats();
    }

    private void UpdateEquipmentStats()
    {
        PlayerSO equipmentsSO = null;
        if(baseStats.equipmentsSO != null)
        {
            equipmentsSO = Instantiate(baseStats.equipmentsSO);
            // 베이스 스탯의 PlayerSO를 복제해서 생성
        }

        CurrentStats = new EquipMentStats { equipmentsSO = equipmentsSO };

        UpdateStats((a, b) => b, baseStats);

        foreach (EquipMentStats modifier in statsModifiers.OrderBy(o => o.statChangeType))
        {
            if(modifier.statChangeType == StatChangeType.Add)
            {
                UpdateStats((o, o1) => o + o1, modifier);
            }
            else if (modifier.statChangeType == StatChangeType.Reduce)
            {
                UpdateStats((o, o1) => o - o1, modifier);
                // AttackDelay 같은 경우 1.5(baseStat) -> 0.3(newModifier) = 1.2
            }
            else if (modifier.statChangeType == StatChangeType.Multiple)
            {
                UpdateStats((o, o1) => o + (o * o1), modifier);
                // % 증가            
            }
            else
            {
                UpdateStats((o,o1) => o1, modifier);
                // 덮어쓰기
            }
        }
    }

    private void UpdateStats(Func<float,float,float> operation, EquipMentStats newModifier)
    {
        if (CurrentStats.equipmentsSO.GetType() != newModifier.equipmentsSO.GetType()) return;

        CurrentStats.equipmentsSO.Health = operation(CurrentStats.equipmentsSO.Health, newModifier.equipmentsSO.Health);
        CurrentStats.equipmentsSO.HealthRegen = operation(CurrentStats.equipmentsSO.HealthRegen, newModifier.equipmentsSO.HealthRegen);
        CurrentStats.equipmentsSO.Defense = operation(CurrentStats.equipmentsSO.Defense, newModifier.equipmentsSO.Defense);
        CurrentStats.equipmentsSO.EvasionProbability = operation(CurrentStats.equipmentsSO.EvasionProbability, newModifier.equipmentsSO.EvasionProbability);
        CurrentStats.equipmentsSO.Attack = operation(CurrentStats.equipmentsSO.Attack, newModifier.equipmentsSO.Attack);
        CurrentStats.equipmentsSO.AttackDelay = operation(CurrentStats.equipmentsSO.AttackDelay, newModifier.equipmentsSO.AttackDelay);
        CurrentStats.equipmentsSO.AttackRange = operation(CurrentStats.equipmentsSO.AttackRange, newModifier.equipmentsSO.AttackRange);
        CurrentStats.equipmentsSO.CriticalProbability = operation(CurrentStats.equipmentsSO.CriticalProbability, newModifier.equipmentsSO.CriticalProbability);
        CurrentStats.equipmentsSO.CriticalMod = operation(CurrentStats.equipmentsSO.CriticalMod, newModifier.equipmentsSO.CriticalMod);
        CurrentStats.equipmentsSO.SpeedMin = operation(CurrentStats.equipmentsSO.SpeedMin, newModifier.equipmentsSO.SpeedMin);
        CurrentStats.equipmentsSO.SpeedMax = operation(CurrentStats.equipmentsSO.SpeedMax, newModifier.equipmentsSO.SpeedMax);
        CurrentStats.equipmentsSO.KnockbackPower = operation(CurrentStats.equipmentsSO.KnockbackPower, newModifier.equipmentsSO.KnockbackPower);
        CurrentStats.equipmentsSO.KnockbackTime = operation(CurrentStats.equipmentsSO.KnockbackTime, newModifier.equipmentsSO.KnockbackTime);
        CurrentStats.equipmentsSO.JumpingForce = operation(CurrentStats.equipmentsSO.JumpingForce, newModifier.equipmentsSO.JumpingForce);
        CurrentStats.equipmentsSO.JumpingCountMax = (int)operation(CurrentStats.equipmentsSO.JumpingCountMax, newModifier.equipmentsSO.JumpingCountMax);
        CurrentStats.equipmentsSO.RollingForce = operation(CurrentStats.equipmentsSO.RollingForce, newModifier.equipmentsSO.RollingForce);
        CurrentStats.equipmentsSO.RollingCoolTime = operation(CurrentStats.equipmentsSO.RollingCoolTime, newModifier.equipmentsSO.RollingCoolTime);
        CurrentStats.equipmentsSO.KcalPerAttack = operation(CurrentStats.equipmentsSO.KcalPerAttack, newModifier.equipmentsSO.KcalPerAttack);
        CurrentStats.equipmentsSO.MaxKcal = operation(CurrentStats.equipmentsSO.MaxKcal, newModifier.equipmentsSO.MaxKcal); 
    }

    public void SetBackWeapon()
    {
        backWeaponPos.SetActive(true);
        HandWeaponPos.SetActive(false);        
    }

    public void SetHandWeapon()
    {
        backWeaponPos.SetActive(false);
        HandWeaponPos.SetActive(true);
    }
}
