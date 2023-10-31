using UnityEngine;

public class RollDataHandler
{
    public bool CanRoll { get; private set; }
    public bool IsInvincible { get; private set; }
    public bool IsRolling { get; private set; }
    private float _rollingCoolTime;
    private float _currentRollingElapsedTime;
    private float _invincibleTime;

    public RollDataHandler(float rollingCoolTime, float invincibleTime)
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
        if (_currentRollingElapsedTime >= _rollingCoolTime)
        {
            CanRoll = true;
            return;
        }

        _currentRollingElapsedTime += Time.deltaTime;
        _currentRollingElapsedTime = 
            _currentRollingElapsedTime > _rollingCoolTime ? 
            _rollingCoolTime : _currentRollingElapsedTime;

        CalculateInvincible();
    }

    public void SetIsRolling(bool isRolling)
    {
        IsRolling = isRolling;
    }

    private void CalculateInvincible()
    {
        if (_currentRollingElapsedTime < _invincibleTime)
            return;

        IsInvincible = false;
    }
}