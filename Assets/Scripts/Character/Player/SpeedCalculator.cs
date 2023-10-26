using System;
using UnityEngine;

public class SpeedCalculator
{
    private float _acceleratingTime;
    private float _movingElapsedTime;

    public SpeedCalculator(float acceleratingTime)
    {
        _acceleratingTime = acceleratingTime;
    }

    public float CalculateSpeed(float speedMin, float speedMax, out float speedRatio, bool isStoped)
    {
        if (isStoped)
        {
            _movingElapsedTime = 0f;
            speedRatio = 0f;
            return 1f;
        }

        _movingElapsedTime += Time.deltaTime;
        speedRatio = _movingElapsedTime / _acceleratingTime;
        speedRatio = speedRatio > 1f ? 1f : speedRatio;
        return Mathf.Lerp(speedMin, speedMax, speedRatio);
    }
}