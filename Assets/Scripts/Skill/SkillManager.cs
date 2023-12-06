using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : CustomSingleton<SkillManager>
{
    public int skillPoint = 8;
    public Dictionary<string, int> baseSkills = new Dictionary<string, int>();
    public Dictionary<string, int> learnedSkills = new Dictionary<string, int>();
    public SkillUse[] skillUse;

    public SkillDataSO hammerData;
    public SkillDataSO knifeData;
    public SkillDataSO psychometricrData;
    public SkillDataSO skinData;

    private Dictionary<StatType, float> _multipleStats;
    private Dictionary<StatType, float> _overrideStats;

    private void Awake()
    {
        foreach (SkillCategoryType enumItem in Enum.GetValues(typeof(SkillCategoryType)))
        {
            baseSkills.Add($"{enumItem}",0);
        }
    }

    public void StopAllSKill()
    {
        if (skillUse == null)
            return;

        foreach (SkillUse su in skillUse)
        {
            su.StopSkill();
        }
    }

    public void SetSkillUes(SkillUse[] SkillUse)
    {
        skillUse = SkillUse;

        foreach (SkillUse su in skillUse)
        {
            su.UpdataSkillData();
        }
    }

    public void UpdateLearn()
    {
        foreach(SkillUse skillUse in skillUse)
        {
            if (learnedSkills.ContainsKey($"{skillUse.skillCategoryType}"))
            {
                skillUse.LearnedSkill();
            }
        }
    }

    #region 변이 상용시 증가되는 스텟 관리

    public void UpdateSkillSO(SkillCategoryType skillCategoryType, StatChange[] statChanges)
    {

        switch (skillCategoryType)
        {
            case SkillCategoryType.Skin:
                UpdateSkillSO(skinData, statChanges);
                break;
            case SkillCategoryType.Knife:
                UpdateSkillSO(knifeData, statChanges);
                break;
            case SkillCategoryType.Hammer:
                UpdateSkillSO(hammerData, statChanges);
                break;
            case SkillCategoryType.Psychometric:
                UpdateSkillSO(psychometricrData, statChanges);
                break;
        }

        skillUse[(int)skillCategoryType].UpdataSkillData();

    }

    private void UpdateSkillSO(SkillDataSO skillDataSO, StatChange[] statChanges)
    {
        _multipleStats = new Dictionary<StatType, float>();
        _overrideStats = new Dictionary<StatType, float>();
        // 기본 분류
        foreach (StatChange statChange in statChanges)
        {
            float a = -1f;
            switch (statChange.statType)
            {
                case StatType.Health:
                    skillDataSO.Health = CalculateStat(StatType.Health, statChange.statsChangeType, skillDataSO.Health, statChange.value);
                    break;
                case StatType.MaxHealth:
                    skillDataSO.MaxHealth = CalculateStat(StatType.MaxHealth, statChange.statsChangeType, skillDataSO.MaxHealth, statChange.value);
                    break;
                case StatType.HealthRegen:
                    skillDataSO.HealthRegen = CalculateStat(StatType.HealthRegen, statChange.statsChangeType, skillDataSO.HealthRegen, statChange.value);
                    break;
                case StatType.Defense:
                    skillDataSO.Defense = CalculateStat(StatType.Defense, statChange.statsChangeType, skillDataSO.Defense, statChange.value);
                    break;
                case StatType.EvasionProbability:
                    skillDataSO.EvasionProbability = CalculateStat(StatType.EvasionProbability, statChange.statsChangeType, skillDataSO.EvasionProbability, statChange.value);
                    break;
                case StatType.InvincibleTime:
                    skillDataSO.InvincibleTime = CalculateStat(StatType.InvincibleTime, statChange.statsChangeType, skillDataSO.InvincibleTime, statChange.value);
                    break;
                case StatType.Attack:
                    skillDataSO.Attack = CalculateStat(StatType.Attack, statChange.statsChangeType, skillDataSO.Attack, statChange.value);
                    break;
                case StatType.AttackDelay:
                    skillDataSO.AttackDelay = CalculateStat(StatType.AttackDelay, statChange.statsChangeType, skillDataSO.AttackDelay, statChange.value);
                    break;
                case StatType.AttackRange:
                    skillDataSO.AttackRange = CalculateStat(StatType.AttackRange, statChange.statsChangeType, skillDataSO.AttackRange, statChange.value);
                    break;
                case StatType.CriticalProbability:
                    skillDataSO.CriticalProbability = CalculateStat(StatType.CriticalProbability, statChange.statsChangeType, skillDataSO.CriticalProbability, statChange.value);
                    break;
                case StatType.CriticalMod:
                    skillDataSO.CriticalMod = CalculateStat(StatType.CriticalMod, statChange.statsChangeType, skillDataSO.CriticalMod, statChange.value);
                    break;
                case StatType.SpeedMin:
                    skillDataSO.SpeedMin = CalculateStat(StatType.SpeedMin, statChange.statsChangeType, skillDataSO.SpeedMin, statChange.value);
                    break;
                case StatType.SpeedMax:
                    skillDataSO.SpeedMax = CalculateStat(StatType.SpeedMax, statChange.statsChangeType, skillDataSO.SpeedMax, statChange.value);
                    break;
                case StatType.KnockbackPower:
                    skillDataSO.KnockbackPower = CalculateStat(StatType.KnockbackPower, statChange.statsChangeType, skillDataSO.KnockbackPower, statChange.value);
                    break;
                case StatType.JumpingForce:
                    skillDataSO.JumpingForce = CalculateStat(StatType.JumpingForce, statChange.statsChangeType, skillDataSO.JumpingForce, statChange.value);
                    break;
                case StatType.JumpingCountMax:
                    skillDataSO.JumpingCountMax = (int)CalculateStat(StatType.JumpingCountMax, statChange.statsChangeType, (float)skillDataSO.JumpingCountMax, statChange.value);
                    break;
                case StatType.RollingForce:
                    skillDataSO.RollingForce = CalculateStat(StatType.RollingForce, statChange.statsChangeType, skillDataSO.RollingForce, statChange.value);
                    break;
                case StatType.RollingCoolTime:
                    skillDataSO.RollingCoolTime = CalculateStat(StatType.RollingCoolTime, statChange.statsChangeType, skillDataSO.RollingCoolTime, statChange.value);
                    break;
                case StatType.KcalPerAttack:
                    skillDataSO.KcalPerAttack = CalculateStat(StatType.KcalPerAttack, statChange.statsChangeType, skillDataSO.KcalPerAttack, statChange.value);
                    break;
                case StatType.Kcal:
                    skillDataSO.Kcal = CalculateStat(StatType.Kcal, statChange.statsChangeType, skillDataSO.Kcal, statChange.value);
                    break;
                case StatType.MaxKcal:
                    skillDataSO.MaxKcal = CalculateStat(StatType.MaxKcal, statChange.statsChangeType, skillDataSO.MaxKcal, statChange.value);
                    break;
                case StatType.WallSlidingTime:
                    skillDataSO.WallSlidingTime = CalculateStat(StatType.WallSlidingTime, statChange.statsChangeType, skillDataSO.WallSlidingTime, statChange.value);
                    break;
                case StatType.KnockbackTime:
                    skillDataSO.KnockbackTime = CalculateStat(StatType.KnockbackTime, statChange.statsChangeType, skillDataSO.KnockbackTime, statChange.value);
                    break;
                case StatType.WallSlidingSpeed:
                    skillDataSO.WallSlidingSpeed = CalculateStat(StatType.WallSlidingSpeed, statChange.statsChangeType, skillDataSO.WallSlidingSpeed, statChange.value);
                    break;
                case StatType.IsInvincible:
                    a = skillDataSO.IsInvincible ? 1 : 0;
                    skillDataSO.IsInvincible = CalculateStat(StatType.IsInvincible, statChange.statsChangeType, a, statChange.value) == 1 ? true : false;
                    break;
                case StatType.IsRanged:
                    a = skillDataSO.IsRanged ? 1 : 0;
                    skillDataSO.IsRanged = CalculateStat(StatType.IsRanged, statChange.statsChangeType, a, statChange.value) == 1 ? true : false;
                    break;
                case StatType.UsingKcal:
                    skillDataSO.UsingKcal = CalculateStat(StatType.UsingKcal, statChange.statsChangeType, skillDataSO.UsingKcal, statChange.value);
                    break;
                case StatType.DurationKcal:
                    skillDataSO.DurationKcal = CalculateStat(StatType.DurationKcal, statChange.statsChangeType, skillDataSO.DurationKcal, statChange.value);
                    break;
                case StatType.DurationTime:
                    skillDataSO.DurationTime = CalculateStat(StatType.DurationTime, statChange.statsChangeType, skillDataSO.DurationTime, statChange.value);
                    break;

            }
        }

        // 곱셈 적용
        if (_multipleStats.Count > 0)
        {
            foreach (var entry in _multipleStats)
            {
                switch (entry.Key)
                {
                    case StatType.Health:
                        skillDataSO.Health = entry.Value < -100f ? 0 : skillDataSO.Health + (float)Math.Round((skillDataSO.Health * entry.Value * 0.01f), 2);
                        break;
                    case StatType.MaxHealth:
                        skillDataSO.MaxHealth = entry.Value < -100f ? 0 : skillDataSO.MaxHealth + (float)Math.Round((skillDataSO.MaxHealth * entry.Value * 0.01f), 2);
                        break;
                    case StatType.HealthRegen:
                        skillDataSO.HealthRegen = entry.Value < -100f ? 0 : skillDataSO.HealthRegen + (float)Math.Round((skillDataSO.HealthRegen * entry.Value * 0.01f), 2);
                        break;
                    case StatType.Defense:
                        skillDataSO.Defense = entry.Value < -100f ? 0 : skillDataSO.Defense + (float)Math.Round((skillDataSO.Defense * entry.Value * 0.01f), 2);
                        break;
                    case StatType.EvasionProbability:
                        skillDataSO.EvasionProbability = entry.Value < -100f ? 0 : skillDataSO.EvasionProbability + (float)Math.Round((skillDataSO.EvasionProbability * entry.Value * 0.01f), 2);
                        break;
                    case StatType.InvincibleTime:
                        skillDataSO.InvincibleTime = entry.Value < -100f ? 0 : skillDataSO.InvincibleTime + (float)Math.Round((skillDataSO.InvincibleTime * entry.Value * 0.01f), 2);
                        break;
                    case StatType.Attack:
                        skillDataSO.Attack = entry.Value < -100f ? 0 : skillDataSO.Attack + (float)Math.Round((skillDataSO.Attack * entry.Value * 0.01f), 2);
                        break;
                    case StatType.AttackDelay:
                        skillDataSO.AttackDelay = entry.Value < -100f ? 0 : skillDataSO.AttackDelay + (float)Math.Round((skillDataSO.AttackDelay * entry.Value * 0.01f), 2);
                        break;
                    case StatType.AttackRange:
                        skillDataSO.AttackRange = entry.Value < -100f ? 0 : skillDataSO.AttackRange + (float)Math.Round((skillDataSO.AttackRange * entry.Value * 0.01f), 2);
                        break;
                    case StatType.CriticalProbability:
                        skillDataSO.CriticalProbability = entry.Value < -100f ? 0 : skillDataSO.CriticalProbability + (float)Math.Round((skillDataSO.CriticalProbability * entry.Value * 0.01f), 2);
                        break;
                    case StatType.CriticalMod:
                        skillDataSO.CriticalMod = entry.Value < -100f ? 0 : skillDataSO.CriticalMod + (float)Math.Round((skillDataSO.CriticalMod * entry.Value * 0.01f), 2);
                        break;
                    case StatType.SpeedMin:
                        skillDataSO.SpeedMin = entry.Value < -100f ? 0 : skillDataSO.SpeedMin + (float)Math.Round((skillDataSO.SpeedMin * entry.Value * 0.01f), 2);
                        break;
                    case StatType.SpeedMax:
                        skillDataSO.SpeedMax = entry.Value < -100f ? 0 : skillDataSO.SpeedMax + (float)Math.Round((skillDataSO.SpeedMax * entry.Value * 0.01f), 2);
                        break;
                    case StatType.KnockbackPower:
                        skillDataSO.KnockbackPower = entry.Value < -100f ? 0 : skillDataSO.KnockbackPower + (float)Math.Round((skillDataSO.KnockbackPower * entry.Value * 0.01f), 2);
                        break;
                    case StatType.JumpingForce:
                        skillDataSO.JumpingForce = entry.Value < -100f ? 0 : skillDataSO.JumpingForce + (float)Math.Round((skillDataSO.JumpingForce * entry.Value * 0.01f), 2);
                        break;
                    case StatType.JumpingCountMax:
                        skillDataSO.JumpingCountMax = entry.Value < -100f ? 0 : (int)(skillDataSO.JumpingCountMax + (int)(skillDataSO.JumpingCountMax * entry.Value * 0.01f));
                        break;
                    case StatType.RollingForce:
                        skillDataSO.RollingForce = entry.Value < -100f ? 0 : skillDataSO.RollingForce + (float)Math.Round((skillDataSO.RollingForce * entry.Value * 0.01f), 2);
                        break;
                    case StatType.RollingCoolTime:
                        skillDataSO.RollingCoolTime = entry.Value < -100f ? 0 : skillDataSO.RollingCoolTime + (float)Math.Round((skillDataSO.RollingCoolTime * entry.Value * 0.01f), 2);
                        break;
                    case StatType.KcalPerAttack:
                        skillDataSO.KcalPerAttack = entry.Value < -100f ? 0 : skillDataSO.KcalPerAttack + (float)Math.Round((skillDataSO.KcalPerAttack * entry.Value * 0.01f), 2);
                        break;
                    case StatType.Kcal:
                        skillDataSO.Kcal = entry.Value < -100f ? 0 : skillDataSO.Kcal + (float)Math.Round((skillDataSO.Kcal * entry.Value * 0.01f), 2);
                        break;
                    case StatType.MaxKcal:
                        skillDataSO.MaxKcal = entry.Value < -100f ? 0 : skillDataSO.MaxKcal + (float)Math.Round((skillDataSO.MaxKcal * entry.Value * 0.01f), 2);
                        break;
                    case StatType.WallSlidingTime:
                        skillDataSO.WallSlidingTime = entry.Value < -100f ? 0 : skillDataSO.WallSlidingTime + (float)Math.Round((skillDataSO.WallSlidingTime * entry.Value * 0.01f), 2);
                        break;
                    case StatType.KnockbackTime:
                        skillDataSO.KnockbackTime = entry.Value < -100f ? 0 : skillDataSO.KnockbackTime + (float)Math.Round((skillDataSO.KnockbackTime * entry.Value * 0.01f), 2);
                        break;
                    case StatType.WallSlidingSpeed:
                        skillDataSO.WallSlidingSpeed = entry.Value < -100f ? 0 : skillDataSO.WallSlidingSpeed + (float)Math.Round((skillDataSO.WallSlidingSpeed * entry.Value * 0.01f), 2);
                        break;
                    case StatType.UsingKcal:
                        skillDataSO.UsingKcal = entry.Value < -100f ? 0 : skillDataSO.UsingKcal + (float)Math.Round((skillDataSO.UsingKcal * entry.Value * 0.01f), 2);
                        break;
                    case StatType.DurationKcal:
                        skillDataSO.DurationKcal = entry.Value < -100f ? 0 : skillDataSO.DurationKcal + (float)Math.Round((skillDataSO.DurationKcal * entry.Value * 0.01f), 2);
                        break;
                    case StatType.DurationTime:
                        skillDataSO.DurationTime = entry.Value < -100f ? 0 : skillDataSO.DurationTime + (float)Math.Round((skillDataSO.DurationTime * entry.Value * 0.01f), 2);
                        break;
                }
            }
        }

        // 덮어쓰기 적용
        if (_overrideStats.Count > 0)
        {
            foreach (var entry in _overrideStats)
            {
                switch (entry.Key)
                {
                    case StatType.Health:
                        skillDataSO.Health = entry.Value;
                        break;
                    case StatType.MaxHealth:
                        skillDataSO.MaxHealth = entry.Value;
                        break;
                    case StatType.HealthRegen:
                        skillDataSO.HealthRegen = entry.Value;
                        break;
                    case StatType.Defense:
                        skillDataSO.Defense = entry.Value;
                        break;
                    case StatType.EvasionProbability:
                        skillDataSO.EvasionProbability = entry.Value;
                        break;
                    case StatType.InvincibleTime:
                        skillDataSO.InvincibleTime = entry.Value;
                        break;
                    case StatType.Attack:
                        skillDataSO.Attack = entry.Value;
                        break;
                    case StatType.AttackDelay:
                        skillDataSO.AttackDelay = entry.Value;
                        break;
                    case StatType.AttackRange:
                        skillDataSO.AttackRange = entry.Value;
                        break;
                    case StatType.CriticalProbability:
                        skillDataSO.CriticalProbability = entry.Value;
                        break;
                    case StatType.CriticalMod:
                        skillDataSO.CriticalMod = entry.Value;
                        break;
                    case StatType.SpeedMin:
                        skillDataSO.SpeedMin = entry.Value;
                        break;
                    case StatType.SpeedMax:
                        skillDataSO.SpeedMax = entry.Value;
                        break;
                    case StatType.KnockbackPower:
                        skillDataSO.KnockbackPower = entry.Value;
                        break;
                    case StatType.JumpingForce:
                        skillDataSO.JumpingForce = entry.Value;
                        break;
                    case StatType.JumpingCountMax:
                        skillDataSO.JumpingCountMax = (int)entry.Value;
                        break;
                    case StatType.RollingForce:
                        skillDataSO.RollingForce = entry.Value;
                        break;
                    case StatType.RollingCoolTime:
                        skillDataSO.RollingCoolTime = entry.Value;
                        break;
                    case StatType.KcalPerAttack:
                        skillDataSO.KcalPerAttack = entry.Value;
                        break;
                    case StatType.Kcal:
                        skillDataSO.Kcal = entry.Value;
                        break;
                    case StatType.MaxKcal:
                        skillDataSO.MaxKcal = entry.Value;
                        break;
                    case StatType.WallSlidingTime:
                        skillDataSO.WallSlidingTime = entry.Value;
                        break;
                    case StatType.KnockbackTime:
                        skillDataSO.KnockbackTime = entry.Value;
                        break;
                    case StatType.WallSlidingSpeed:
                        skillDataSO.WallSlidingSpeed = entry.Value;
                        break;
                    case StatType.UsingKcal:
                        skillDataSO.UsingKcal = entry.Value;
                        break;
                    case StatType.DurationKcal:
                        skillDataSO.DurationKcal = entry.Value;
                        break;
                    case StatType.DurationTime:
                        skillDataSO.DurationTime = entry.Value;
                        break;
                }
            }
        }
    }

    private float CalculateStat(StatType statType, StatsChangeType statsChangeType, float a, float b)
    {
        switch (statsChangeType)
        {
            case StatsChangeType.Add:
                return a + b;
            case StatsChangeType.Subtract:
                Debug.Log($"{a}\t{b}");
                return a - b <= 0f ? 0 : a - b;
            case StatsChangeType.Multiple:
                if (!_multipleStats.ContainsKey(statType))
                {
                    _multipleStats.Add(statType, b);
                }
                else
                {
                    _multipleStats[statType] += b;
                }
                return a;
            case StatsChangeType.Divide:
                if (!_multipleStats.ContainsKey(statType))
                {
                    _multipleStats.Add(statType, -b);
                }
                else
                {
                    _multipleStats[statType] -= b;
                }
                return a;
            case StatsChangeType.Override:
                if (!_overrideStats.ContainsKey(statType))
                {
                    _overrideStats.Add(statType, b);
                }
                else
                {
                    if (_overrideStats[statType] < b)
                    {
                        _overrideStats[statType] = b;
                    }
                }
                return a;
            default:
                return a;
        }
    }

    #endregion

    public void AllOffSkill()
    {
        foreach(SkillUse skillUse in skillUse)
        {
            skillUse.StopSkillRightAway();
        }
    }
}
