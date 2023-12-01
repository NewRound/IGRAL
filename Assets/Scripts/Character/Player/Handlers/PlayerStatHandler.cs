using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

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
    , [Description("소비 칼로리")] UsingKcal
    , [Description("초당 소비되는 칼로리")] DurationKcal
    , [Description("유지 시간")] DurationTime
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

    public StatChange(StatsChangeType _changeType, StatType _statType, float _value)
    {
        statsChangeType = _changeType;
        statType = _statType;
        value = _value;        
    }
}

public class PlayerStatHandler : IDamageable, IBurnable
{
    public PlayerSO Data { get; private set; }
    private PlayerSO BackData;
    private PlayerSO _baseData;

    private Dictionary<StatType, float> _multipleStats;
    private Dictionary<StatType, float> _overrideStats;

    public event Action DieAction;
    public event Action DamagedAction;

    private Vector3 yOffset = new Vector3(0f, 1.6f, 0f);

    public PlayerStatHandler(PlayerSO data)
    {
        _baseData = data;
        Data = UnityEngine.Object.Instantiate(_baseData);
    }

    public void Damaged(float damage)
    {
        float curValue = Mathf.Max(Data.Health - damage, 0.0f);
        Data.Health = curValue;
        DamagedAction?.Invoke();

        if (curValue == 0.0f)
        {
            DieAction?.Invoke();
        }

        GameObject damagedTxt = ObjectPoolingManager.Instance.GetGameObject(ObjectPoolType.PlayerDamagedTxt);
        damagedTxt.transform.position = GameManager.Instance.PlayerTransform.position + yOffset;
        damagedTxt.GetComponent<DamagedTxt>()._damage = damage;

        EffectManager.Instance.ShowEffect(GameManager.Instance.PlayerTransform.position + yOffset, EffectType.Damaged);
        AudioManager.Instance.PlaySFX(SFXType.Damaged0);
    }

    public void Recovery(float damage)
    {
        float curValue = Mathf.Min(Data.Health + damage, Data.MaxHealth);
        Data.Health = curValue;
    }

    public void BurnKcal(float kcal)
    {
        float curValue = Mathf.Max(Data.Kcal - kcal, 0.0f);
        Data.Kcal = curValue;
    }

    public void RecoveryKcal(float kcal)
    {
        float curValue = Mathf.Min(Data.Kcal + kcal, Data.MaxKcal);
        Data.Kcal = curValue;
    }

    public void UpdateStats(StatChange[] statChanges)
    {
        // 데이터 초기화
        foreach (StatType enumItem in Enum.GetValues(typeof(StatType)))
        {
            switch (enumItem)
            {
                case StatType.MaxHealth:
                    Data.MaxHealth = _baseData.MaxHealth;
                    break;
                case StatType.HealthRegen:
                    Data.HealthRegen = _baseData.HealthRegen;
                    break;
                case StatType.Defense:
                    Data.Defense = _baseData.Defense;
                    break;
                case StatType.EvasionProbability:
                    Data.EvasionProbability = _baseData.EvasionProbability;
                    break;
                case StatType.InvincibleTime:
                    Data.InvincibleTime = _baseData.InvincibleTime;
                    break;
                case StatType.Attack:
                    Data.Attack = _baseData.Attack;
                    break;
                case StatType.AttackDelay:
                    Data.AttackDelay = _baseData.AttackDelay;
                    break;
                case StatType.AttackRange:
                    Data.AttackRange = _baseData.AttackRange;
                    break;
                case StatType.CriticalProbability:
                    Data.CriticalProbability = _baseData.CriticalProbability;
                    break;
                case StatType.CriticalMod:
                    Data.CriticalMod = _baseData.CriticalMod;
                    break;
                case StatType.SpeedMin:
                    Data.SpeedMin = _baseData.SpeedMin;
                    break;
                case StatType.SpeedMax:
                    Data.SpeedMax = _baseData.SpeedMax;
                    break;
                case StatType.KnockbackPower:
                    Data.KnockbackPower = _baseData.KnockbackPower;
                    break;
                case StatType.JumpingForce:
                    Data.JumpingForce = _baseData.JumpingForce;
                    break;
                case StatType.JumpingCountMax:
                    Data.JumpingCountMax = _baseData.JumpingCountMax;
                    break;
                case StatType.RollingForce:
                    Data.RollingForce = _baseData.RollingForce;
                    break;
                case StatType.RollingCoolTime:
                    Data.RollingCoolTime = _baseData.RollingCoolTime;
                    break;
                case StatType.KcalPerAttack:
                    Data.KcalPerAttack = _baseData.KcalPerAttack;
                    break;
                case StatType.MaxKcal:
                    Data.MaxKcal = _baseData.MaxKcal;
                    break;
                case StatType.WallSlidingTime:
                    Data.WallSlidingTime = _baseData.WallSlidingTime;
                    break;
                case StatType.KnockbackTime:
                    Data.KnockbackTime = _baseData.KnockbackTime;
                    break;
                case StatType.WallSlidingSpeed:
                    Data.WallSlidingSpeed = _baseData.WallSlidingSpeed;
                    break;
                case StatType.IsInvincible:
                    Data.IsInvincible = _baseData.IsInvincible;
                    break;
                case StatType.IsRanged:
                    Data.IsRanged = _baseData.IsRanged;
                    break;
            }
        }

        _multipleStats = new Dictionary<StatType, float>();
        _overrideStats = new Dictionary<StatType, float>();

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
        if(_multipleStats.Count > 0)
        {
            foreach (var entry in _multipleStats)
            {
                switch (entry.Key)
                {
                    case StatType.Health:
                        Data.Health = entry.Value < -100f ? 0 : Data.Health + (float)Math.Round((Data.Health * entry.Value * 0.01f), 2);
                        break;
                    case StatType.MaxHealth:
                        Data.MaxHealth = entry.Value < -100f ? 0 : Data.MaxHealth + (float)Math.Round((Data.MaxHealth * entry.Value * 0.01f), 2);
                        break;
                    case StatType.HealthRegen:
                        Data.HealthRegen = entry.Value < -100f ? 0 : Data.HealthRegen + (float)Math.Round((Data.HealthRegen * entry.Value * 0.01f), 2);
                        break;
                    case StatType.Defense:
                        Data.Defense = entry.Value < -100f ? 0 : Data.Defense + (float)Math.Round((Data.Defense * entry.Value * 0.01f), 2);
                        break;
                    case StatType.EvasionProbability:
                        Data.EvasionProbability = entry.Value < -100f ? 0 : Data.EvasionProbability + (float)Math.Round((Data.EvasionProbability * entry.Value * 0.01f), 2);
                        break;
                    case StatType.InvincibleTime:
                        Data.InvincibleTime = entry.Value < -100f ? 0 : Data.InvincibleTime + (float)Math.Round((Data.InvincibleTime * entry.Value * 0.01f), 2);
                        break;
                    case StatType.Attack:
                        Data.Attack = entry.Value < -100f ? 0 : Data.Attack + (float)Math.Round((Data.Attack * entry.Value * 0.01f), 2);
                        break;
                    case StatType.AttackDelay:
                        Data.AttackDelay = entry.Value < -100f ? 0 : Data.AttackDelay + (float)Math.Round((Data.AttackDelay * entry.Value * 0.01f), 2);
                        break;
                    case StatType.AttackRange:
                        Data.AttackRange = entry.Value < -100f ? 0 : Data.AttackRange + (float)Math.Round((Data.AttackRange * entry.Value * 0.01f), 2);
                        break;
                    case StatType.CriticalProbability:
                        Data.CriticalProbability = entry.Value < -100f ? 0 : Data.CriticalProbability + (float)Math.Round((Data.CriticalProbability * entry.Value * 0.01f), 2);
                        break;
                    case StatType.CriticalMod:
                        Data.CriticalMod = entry.Value < -100f ? 0 : Data.CriticalMod + (float)Math.Round((Data.CriticalMod * entry.Value * 0.01f), 2);
                        break;
                    case StatType.SpeedMin:
                        Data.SpeedMin = entry.Value < -100f ? 0 : Data.SpeedMin + (float)Math.Round((Data.SpeedMin * entry.Value * 0.01f), 2);
                        break;
                    case StatType.SpeedMax:
                        Data.SpeedMax = entry.Value < -100f ? 0 : Data.SpeedMax + (float)Math.Round((Data.SpeedMax * entry.Value * 0.01f), 2);
                        break;
                    case StatType.KnockbackPower:
                        Data.KnockbackPower = entry.Value < -100f ? 0 : Data.KnockbackPower + (float)Math.Round((Data.KnockbackPower * entry.Value * 0.01f), 2);
                        break;
                    case StatType.JumpingForce:
                        Data.JumpingForce = entry.Value < -100f ? 0 : Data.JumpingForce + (float)Math.Round((Data.JumpingForce * entry.Value * 0.01f), 2);
                        break;
                    case StatType.JumpingCountMax:
                        Data.JumpingCountMax = entry.Value < -100f ? 0 : (int)(Data.JumpingCountMax + (int)(Data.JumpingCountMax * entry.Value * 0.01f));
                        break;
                    case StatType.RollingForce:
                        Data.RollingForce = entry.Value < -100f ? 0 : Data.RollingForce + (float)Math.Round((Data.RollingForce * entry.Value * 0.01f), 2);
                        break;
                    case StatType.RollingCoolTime:
                        Data.RollingCoolTime = entry.Value < -100f ? 0 : Data.RollingCoolTime + (float)Math.Round((Data.RollingCoolTime * entry.Value * 0.01f), 2);
                        break;
                    case StatType.KcalPerAttack:
                        Data.KcalPerAttack = entry.Value < -100f ? 0 : Data.KcalPerAttack + (float)Math.Round((Data.KcalPerAttack * entry.Value * 0.01f), 2);
                        break;
                    case StatType.Kcal:
                        Data.Kcal = entry.Value < -100f ? 0 : Data.Kcal + (float)Math.Round((Data.Kcal * entry.Value * 0.01f), 2);
                        break;
                    case StatType.MaxKcal:
                        Data.MaxKcal = entry.Value < -100f ? 0 : Data.MaxKcal + (float)Math.Round((Data.MaxKcal * entry.Value * 0.01f), 2);
                        break;
                    case StatType.WallSlidingTime:
                        Data.WallSlidingTime = entry.Value < -100f ? 0 : Data.WallSlidingTime + (float)Math.Round((Data.WallSlidingTime * entry.Value * 0.01f), 2);
                        break;
                    case StatType.KnockbackTime:
                        Data.KnockbackTime = entry.Value < -100f ? 0 : Data.KnockbackTime + (float)Math.Round((Data.KnockbackTime * entry.Value * 0.01f), 2);
                        break;
                    case StatType.WallSlidingSpeed:
                        Data.WallSlidingSpeed = entry.Value < -100f ? 0 : Data.WallSlidingSpeed + (float)Math.Round((Data.WallSlidingSpeed * entry.Value * 0.01f), 2);
                        break;
                }
            }
        }

        // 덮어쓰기 적용
        if(_overrideStats.Count > 0)
        {
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
        BackData = UnityEngine.Object.Instantiate(Data);
    }

    private void UpdateStats(SkillDataSO skillDataSO)
    {
        foreach (StatType enumItem in Enum.GetValues(typeof(StatType)))
        {
            switch (enumItem)
            {
                case StatType.MaxHealth:
                    Data.MaxHealth += skillDataSO.MaxHealth;
                    break;
                case StatType.HealthRegen:
                    Data.HealthRegen += skillDataSO.HealthRegen;
                    break;
                case StatType.Defense:
                    Data.Defense += skillDataSO.Defense;
                    break;
                case StatType.EvasionProbability:
                    Data.EvasionProbability += skillDataSO.EvasionProbability;
                    break;
                case StatType.InvincibleTime:
                    Data.InvincibleTime += skillDataSO.InvincibleTime;
                    break;
                case StatType.Attack:
                    Data.Attack += skillDataSO.Attack;
                    break;
                case StatType.AttackDelay:
                    Data.AttackDelay += skillDataSO.AttackDelay;
                    break;
                case StatType.AttackRange:
                    Data.AttackRange += skillDataSO.AttackRange;
                    break;
                case StatType.CriticalProbability:
                    Data.CriticalProbability += skillDataSO.CriticalProbability;
                    break;
                case StatType.CriticalMod:
                    Data.CriticalMod += skillDataSO.CriticalMod;
                    break;
                case StatType.SpeedMin:
                    Data.SpeedMin += skillDataSO.SpeedMin;
                    break;
                case StatType.SpeedMax:
                    Data.SpeedMax += skillDataSO.SpeedMax;
                    break;
                case StatType.KnockbackPower:
                    Data.KnockbackPower += skillDataSO.KnockbackPower;
                    break;
                case StatType.JumpingForce:
                    Data.JumpingForce += skillDataSO.JumpingForce;
                    break;
                case StatType.JumpingCountMax:
                    Data.JumpingCountMax += skillDataSO.JumpingCountMax;
                    break;
                case StatType.RollingForce:
                    Data.RollingForce += skillDataSO.RollingForce;
                    break;
                case StatType.RollingCoolTime:
                    Data.RollingCoolTime += skillDataSO.RollingCoolTime;
                    break;
                case StatType.KcalPerAttack:
                    Data.KcalPerAttack += skillDataSO.KcalPerAttack;
                    break;
                case StatType.MaxKcal:
                    Data.MaxKcal += skillDataSO.MaxKcal;
                    break;
                case StatType.WallSlidingTime:
                    Data.WallSlidingTime += skillDataSO.WallSlidingTime;
                    break;
                case StatType.KnockbackTime:
                    Data.KnockbackTime += skillDataSO.KnockbackTime;
                    break;
                case StatType.WallSlidingSpeed:
                    Data.WallSlidingSpeed += skillDataSO.WallSlidingSpeed;
                    break;
            }
        }
    }

    public void UpdateSkillStat(MutantType mutantType)
    {
        switch(mutantType) 
        {
            case MutantType.None:
                UIInventory.Instance.UpdateEquipUI();
                break;
            case MutantType.Stone:
                UpdateStats(SkillManager.Instance.hammerData);
                break;
            case MutantType.Blade:
                UpdateStats(SkillManager.Instance.knifeData);
                break;
            case MutantType.Sheld:
                UpdateStats(SkillManager.Instance.psychometricrData);
                break;
            case MutantType.Skin:
                UpdateStats(SkillManager.Instance.skinData);
                break;

        }
        UIInventory.Instance.UpdateStats();
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

    public void UpdateInvincible(bool isInvincible)
    {
        Data.IsInvincible = isInvincible;
    }

    public void UpdateIsRanged(bool isRanged)
    {
        Data.IsRanged = isRanged;
    }
}
