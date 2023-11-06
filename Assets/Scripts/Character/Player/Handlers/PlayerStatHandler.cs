using System;
using System.Collections.Generic;
using System.ComponentModel;

public enum StatType
{
    [Description("최대 체력")] MaxHealth
    , [Description("체력 회복")] HealthRegen
    , [Description("최대 칼로리")] MaxKcal
    , [Description("공격당 칼로리 회복")] KcalPerAttack
    , [Description("방어력")] Defense 
    , [Description("회피")] EvasionProbability 
    , [Description("무적 시간")] InvincibleTime 
    , [Description("공격력")] Attack 
    , [Description("공격 딜레이")] AttackDelay 
    , [Description("공격 범위")] AttackRange 
    , [Description("크리 확률")] CriticalProbability 
    , [Description("크리 데미지")] CriticalMod 
    , [Description("최소 이동속도")] SpeedMin 
    , [Description("최대 이동속도")] SpeedMax 
    , [Description("넉백")] KnockbackPower
    , [Description("넉백 유지 시간")] KnockbackTime
    , [Description("점프")] JumpingForce 
    , [Description("점프 횟수")] JumpingCountMax 

    , [Description("구르기 범위")] RollingForce 
    , [Description("구르기 시간")] RollingCoolTime

    , [Description("벽점프 유지 시간")] WallSlidingTime
    , [Description("현재 칼로리")] Kcal
    , [Description("벽점프 낙하속도")] WallSlidingSpeed

    , [Description("현재 체력")] Health
    , [Description("무적 여부")] IsInvincible
    , [Description("원거리 여부")] IsRanged

    , Max
}

public enum StatsChangeType
{
    Add
    , Subtract
    , Multiple
    , Divide    
    , Override
}

[Serializable]
public class StatChange
{
    // bool 인경우 0(false), 1(true)
    // Multiple, Divide의 입력받은 수치를 한번에 계산시 사용
    // ex) 
    // MaxHealth, Multiple, 5/ MaxHealth, Multiple, 15/ MaxHealth, Divide, 5
    // 5 + 15 - 5 = 15 -> MaxHealth + MaxHealth * 0.15
    // Override는 가장 큰 값이 항상 적용

    public StatsChangeType statsChangeType;
    public StatType statType;
    public float value;
}

public class PlayerStatHandler : IDamageable
{
    public PlayerSO Data { get; private set; }

    private Dictionary<StatType, float> _multipleStats;
    private Dictionary<StatType, float> _overrideStats;

    public PlayerStatHandler(PlayerSO data)
    {
        Data = data;
    }

    public void Damaged(float damage)
    {
        Data.Health -= damage;
    }

    public void UpdateStats(StatChange[] statChanges)
    {
        _multipleStats = new Dictionary<StatType, float>();

        // 기본 분류
        foreach (StatChange statChange in statChanges)
        {
            float a = -1f;
            switch(statChange.statType) 
            {
                case StatType.Health:
                    Data.Health = CalculateStat(StatType.Health, statChange.statsChangeType, Data.Health, statChange.value);
                    break;
                case StatType.MaxHealth:
                    Data.MaxHealth = CalculateStat(StatType.MaxHealth, statChange.statsChangeType, Data.MaxHealth, statChange.value);
                    break;
                case StatType.HealthRegen:
                    Data.HealthRegen = CalculateStat(StatType.HealthRegen, statChange.statsChangeType, Data.HealthRegen, statChange.value);
                    break;
                case StatType.Defense:
                    Data.Defense = CalculateStat(StatType.Defense, statChange.statsChangeType, Data.Defense, statChange.value);
                    break;
                case StatType.EvasionProbability:
                    Data.EvasionProbability = CalculateStat(StatType.EvasionProbability, statChange.statsChangeType, Data.EvasionProbability, statChange.value);
                    break;
                case StatType.InvincibleTime:
                    Data.InvincibleTime = CalculateStat(StatType.InvincibleTime, statChange.statsChangeType, Data.InvincibleTime, statChange.value);
                    break;
                case StatType.Attack:
                    Data.Attack = CalculateStat(StatType.Attack, statChange.statsChangeType, Data.Attack, statChange.value);
                    break;
                case StatType.AttackDelay:
                    Data.AttackDelay = CalculateStat(StatType.AttackDelay, statChange.statsChangeType, Data.AttackDelay, statChange.value);
                    break;
                case StatType.AttackRange:
                    Data.AttackRange = CalculateStat(StatType.AttackRange, statChange.statsChangeType, Data.AttackRange, statChange.value);
                    break;
                case StatType.CriticalProbability:
                    Data.CriticalProbability = CalculateStat(StatType.CriticalProbability, statChange.statsChangeType, Data.CriticalProbability, statChange.value);
                    break;
                case StatType.CriticalMod:
                    Data.CriticalMod = CalculateStat(StatType.CriticalMod, statChange.statsChangeType, Data.CriticalMod, statChange.value);
                    break;
                case StatType.SpeedMin:
                    Data.SpeedMin = CalculateStat(StatType.SpeedMin, statChange.statsChangeType, Data.SpeedMin, statChange.value);
                    break;
                case StatType.SpeedMax:
                    Data.SpeedMax = CalculateStat(StatType.SpeedMax, statChange.statsChangeType, Data.SpeedMax, statChange.value);
                    break;
                case StatType.KnockbackPower:
                    Data.KnockbackPower = CalculateStat(StatType.KnockbackPower, statChange.statsChangeType, Data.KnockbackPower, statChange.value);
                    break;
                case StatType.JumpingForce:
                    Data.JumpingForce = CalculateStat(StatType.JumpingForce, statChange.statsChangeType, Data.JumpingForce, statChange.value);
                    break;
                case StatType.JumpingCountMax:
                    Data.JumpingCountMax = (int)CalculateStat(StatType.JumpingCountMax, statChange.statsChangeType, (float)Data.JumpingCountMax, statChange.value);
                    break;
                case StatType.RollingForce:
                    Data.RollingForce = CalculateStat(StatType.RollingForce, statChange.statsChangeType, Data.RollingForce, statChange.value);
                    break;
                case StatType.RollingCoolTime:
                    Data.RollingCoolTime = CalculateStat(StatType.RollingCoolTime, statChange.statsChangeType, Data.RollingCoolTime, statChange.value);
                    break;
                case StatType.KcalPerAttack:
                    Data.KcalPerAttack = CalculateStat(StatType.KcalPerAttack, statChange.statsChangeType, Data.KcalPerAttack, statChange.value);
                    break;
                case StatType.Kcal:
                    Data.Kcal = CalculateStat(StatType.Kcal, statChange.statsChangeType, Data.Kcal, statChange.value);
                    break;
                case StatType.MaxKcal:
                    Data.MaxKcal = CalculateStat(StatType.MaxKcal, statChange.statsChangeType, Data.MaxKcal, statChange.value);
                    break;
                case StatType.WallSlidingTime:
                    Data.WallSlidingTime = CalculateStat(StatType.WallSlidingTime,statChange.statsChangeType, Data.WallSlidingTime, statChange.value);
                    break;
                case StatType.KnockbackTime:
                    Data.KnockbackTime = CalculateStat(StatType.KnockbackTime,statChange.statsChangeType, Data.KnockbackTime, statChange.value);
                    break;
                case StatType.WallSlidingSpeed:
                    Data.WallSlidingSpeed = CalculateStat(StatType.WallSlidingSpeed,statChange.statsChangeType, Data.WallSlidingSpeed, statChange.value);
                    break;
                case StatType.IsInvincible:
                    a = Data.IsInvincible ? 1 : 0;
                    Data.IsInvincible = CalculateStat(StatType.IsInvincible, statChange.statsChangeType, a, statChange.value) == 1 ? true : false;
                    break;
                case StatType.IsRanged:
                    a = Data.IsRanged ? 1 : 0;
                    Data.IsRanged = CalculateStat(StatType.IsRanged, statChange.statsChangeType, a, statChange.value) == 1 ? true : false;
                    break;
            }
        }

        // 곱셈 적용
        foreach(var entry in _multipleStats)
        {
            switch (entry.Key)
            {
                case StatType.Health:
                    Data.Health = entry.Value < -100f ? 0 : Data.Health + (Data.Health * entry.Value * 0.01f);
                    break;
                case StatType.MaxHealth:
                    Data.MaxHealth = entry.Value < -100f ? 0 : Data.MaxHealth + (Data.MaxHealth * entry.Value * 0.01f);
                    break;
                case StatType.HealthRegen:
                    Data.HealthRegen = entry.Value < -100f ? 0 : Data.HealthRegen + (Data.HealthRegen * entry.Value * 0.01f);
                    break;
                case StatType.Defense:
                    Data.Defense = entry.Value < -100f ? 0 : Data.Defense + (Data.Defense * entry.Value * 0.01f);
                    break;
                case StatType.EvasionProbability:
                    Data.EvasionProbability = entry.Value < -100f ? 0 : Data.EvasionProbability + (Data.EvasionProbability * entry.Value * 0.01f);
                    break;
                case StatType.InvincibleTime:
                    Data.InvincibleTime = entry.Value < -100f ? 0 : Data.InvincibleTime + (Data.InvincibleTime * entry.Value * 0.01f);
                    break;
                case StatType.Attack:
                    Data.Attack = entry.Value < -100f ? 0 : Data.Attack + (Data.Attack * entry.Value * 0.01f);
                    break;
                case StatType.AttackDelay:
                    Data.AttackDelay = entry.Value < -100f ? 0 : Data.AttackDelay + (Data.AttackDelay * entry.Value * 0.01f);
                    break;
                case StatType.AttackRange:
                    Data.AttackRange = entry.Value < -100f ? 0 : Data.AttackRange + (Data.AttackRange * entry.Value * 0.01f);
                    break;
                case StatType.CriticalProbability:
                    Data.CriticalProbability = entry.Value < -100f ? 0 : Data.CriticalProbability + (Data.CriticalProbability * entry.Value * 0.01f);
                    break;
                case StatType.CriticalMod:
                    Data.CriticalMod = entry.Value < -100f ? 0 : Data.CriticalMod + (Data.CriticalMod * entry.Value * 0.01f);
                    break;
                case StatType.SpeedMin:
                    Data.SpeedMin = entry.Value < -100f ? 0 : Data.SpeedMin + (Data.SpeedMin * entry.Value * 0.01f);
                    break;
                case StatType.SpeedMax:
                    Data.SpeedMax = entry.Value < -100f ? 0 : Data.SpeedMax + (Data.SpeedMax * entry.Value * 0.01f);
                    break;
                case StatType.KnockbackPower:
                    Data.KnockbackPower = entry.Value < -100f ? 0 : Data.KnockbackPower + (Data.KnockbackPower * entry.Value * 0.01f);
                    break;
                case StatType.JumpingForce:
                    Data.JumpingForce = entry.Value < -100f ? 0 : Data.JumpingForce + (Data.JumpingForce * entry.Value * 0.01f);
                    break;
                case StatType.JumpingCountMax:
                    Data.JumpingCountMax = entry.Value < -100f ? 0 : (int)(Data.JumpingCountMax + (Data.JumpingCountMax * entry.Value * 0.01f));
                    break;
                case StatType.RollingForce:
                    Data.RollingForce = entry.Value < -100f ? 0 : Data.RollingForce + (Data.RollingForce * entry.Value * 0.01f);
                    break;
                case StatType.RollingCoolTime:
                    Data.RollingCoolTime = entry.Value < -100f ? 0 : Data.RollingCoolTime + (Data.RollingCoolTime * entry.Value * 0.01f);
                    break;
                case StatType.KcalPerAttack:
                    Data.KcalPerAttack = entry.Value < -100f ? 0 : Data.KcalPerAttack + (Data.KcalPerAttack * entry.Value * 0.01f);
                    break;
                case StatType.Kcal:
                    Data.Kcal = entry.Value < -100f ? 0 : Data.Kcal + (Data.Kcal * entry.Value * 0.01f);
                    break;
                case StatType.MaxKcal:
                    Data.MaxKcal = entry.Value < -100f ? 0 : Data.MaxKcal + (Data.MaxKcal * entry.Value * 0.01f);
                    break;
                case StatType.WallSlidingTime:
                    Data.WallSlidingTime = entry.Value < -100f ? 0 : Data.WallSlidingTime + (Data.WallSlidingTime * entry.Value * 0.01f);
                    break;
                case StatType.KnockbackTime:
                    Data.KnockbackTime = entry.Value < -100f ? 0 : Data.KnockbackTime + (Data.KnockbackTime * entry.Value * 0.01f);
                    break;
                case StatType.WallSlidingSpeed:
                    Data.WallSlidingSpeed = entry.Value < -100f ? 0 : Data.WallSlidingSpeed + (Data.WallSlidingSpeed * entry.Value * 0.01f);
                    break;
            }
        }

        // 덮어쓰기 적용
        foreach (var entry in _overrideStats)
        {
            switch (entry.Key)
            {
                case StatType.Health:
                    Data.Health = entry.Value;
                    break;
                case StatType.MaxHealth:
                    Data.MaxHealth = entry.Value;
                    break;
                case StatType.HealthRegen:
                    Data.HealthRegen = entry.Value;
                    break;
                case StatType.Defense:
                    Data.Defense = entry.Value;
                    break;
                case StatType.EvasionProbability:
                    Data.EvasionProbability = entry.Value;
                    break;
                case StatType.InvincibleTime:
                    Data.InvincibleTime = entry.Value;
                    break;
                case StatType.Attack:
                    Data.Attack = entry.Value;
                    break;
                case StatType.AttackDelay:
                    Data.AttackDelay = entry.Value;
                    break;
                case StatType.AttackRange:
                    Data.AttackRange = entry.Value;
                    break;
                case StatType.CriticalProbability:
                    Data.CriticalProbability = entry.Value;
                    break;
                case StatType.CriticalMod:
                    Data.CriticalMod = entry.Value;
                    break;
                case StatType.SpeedMin:
                    Data.SpeedMin = entry.Value;
                    break;
                case StatType.SpeedMax:
                    Data.SpeedMax = entry.Value;
                    break;
                case StatType.KnockbackPower:
                    Data.KnockbackPower = entry.Value;
                    break;
                case StatType.JumpingForce:
                    Data.JumpingForce = entry.Value;
                    break;
                case StatType.JumpingCountMax:
                    Data.JumpingCountMax = (int)entry.Value;
                    break;
                case StatType.RollingForce:
                    Data.RollingForce = entry.Value;
                    break;
                case StatType.RollingCoolTime:
                    Data.RollingCoolTime = entry.Value;
                    break;
                case StatType.KcalPerAttack:
                    Data.KcalPerAttack = entry.Value;
                    break;
                case StatType.Kcal:
                    Data.Kcal = entry.Value;
                    break;
                case StatType.MaxKcal:
                    Data.MaxKcal = entry.Value;
                    break;
                case StatType.WallSlidingTime:
                    Data.WallSlidingTime = entry.Value;
                    break;
                case StatType.KnockbackTime:
                    Data.KnockbackTime = entry.Value;
                    break;
                case StatType.WallSlidingSpeed:
                    Data.WallSlidingSpeed = entry.Value;
                    break;
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
                return a - b < 0 ? 0 : a - b;
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
                    if(_overrideStats[statType] < b)
                    {
                        _overrideStats[statType] = b;
                    }
                }
                return a;
            default:
                return a;
        }
    }
}
