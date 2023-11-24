using UnityEngine;

public class SpeedCalculator
{
    private float _acceleratingTime;
    private float _movingElapsedTime;

    public SpeedCalculator(float acceleratingTime = 1f)
    {
        _acceleratingTime = acceleratingTime;
    }

    public float CalculateSpeed(float speedMin, float speedMax, out float speedRatio, bool isStopped)
    {
        if (isStopped)
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

    public float CalculateSpeed(float speedMin, float speedMax, out float speedRatio, bool isStopped, bool isPatrolState)
    {
        if (isStopped)
        {
            _movingElapsedTime = 0f;
            speedRatio = 0f;
            return 1f;
        }

        _movingElapsedTime += Time.deltaTime;
        speedRatio = _movingElapsedTime / _acceleratingTime;
        
        if (isPatrolState)
            speedRatio = speedRatio > 0.5f ? 0.5f : speedRatio;
        else
            speedRatio = speedRatio > 1f ? 1f : speedRatio;

        return Mathf.Lerp(speedMin, speedMax, speedRatio);
    }
}