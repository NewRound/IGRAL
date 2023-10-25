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

    public float CalculateSpeed(float speedMin, float speedMax, bool isStoped)
    {
        if (isStoped)
        {
            _movingElapsedTime = 0f;
            return 1f;
        }

        _movingElapsedTime += Time.deltaTime;
        float movingRatio = _movingElapsedTime / _acceleratingTime;
        movingRatio = movingRatio > 1f ? 1f : movingRatio;
        Debug.Log(movingRatio);
        return Mathf.Lerp(speedMin, speedMax, movingRatio);
    }
}