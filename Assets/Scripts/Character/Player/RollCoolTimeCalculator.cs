using UnityEngine;

public class RollCoolTimeCalculator
{
    public bool CanRoll { get; private set; }
    public bool IsInvincible { get; private set; }
    private float _rollingCoolTime;
    private float _currentRollingElapsedTime;
    private float _invincibleTime;

    public RollCoolTimeCalculator(float rollingCoolTime, float invincibleTime)
    {
        SetRollingCoolTime(rollingCoolTime);
        _currentRollingElapsedTime = rollingCoolTime;
        _invincibleTime = invincibleTime;
        CanRoll = true;
    }

    public void SetRollingCoolTime(float rollingCoolTime)
    {
        _rollingCoolTime = rollingCoolTime;
    }

    public void ResetCurrentRollingElapsedTime()
    {
        CanRoll = false;
        _currentRollingElapsedTime = 0f;
        IsInvincible = true;
    }

    public void CalculateCoolTime()
    {
        Debug.Log(IsInvincible);
        if (_currentRollingElapsedTime >= _rollingCoolTime)
        {
            CanRoll = true;
            return;
        }

        _currentRollingElapsedTime += Time.deltaTime;
        _currentRollingElapsedTime = _currentRollingElapsedTime > _rollingCoolTime ? _rollingCoolTime : _currentRollingElapsedTime;

        CalculateInvincible();
    }

    private void CalculateInvincible()
    {
        if (_currentRollingElapsedTime < _invincibleTime)
            return;

        IsInvincible = false;
    }
}