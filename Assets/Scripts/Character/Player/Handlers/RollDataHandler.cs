using UnityEngine;

public class RollDataHandler
{
    public bool CanRoll { get; private set; }
    public bool IsInvincible { get; private set; }
    public bool IsRolling { get; private set; }
    private float _rollingCoolTime;
    private float _currentRollingElapsedTime;
    private float _invincibleTime;

    private PlayerStatHandler _playerStatHandler;

    public RollDataHandler(PlayerStatHandler playerStatHandler)
    {
        _playerStatHandler = playerStatHandler;
        SetRollingCoolTime(_playerStatHandler.Data.RollingCoolTime);
        _currentRollingElapsedTime = _playerStatHandler.Data.RollingCoolTime;
        _invincibleTime = _playerStatHandler.Data.InvincibleTime;
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
        _playerStatHandler.UpdateInvincible(IsInvincible);
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
        _playerStatHandler.UpdateInvincible(IsInvincible);
    }
}