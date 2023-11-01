using UnityEngine;

public class SpeedCalculator
{
    protected float acceleratingTime;
    protected float movingElapsedTime;

    public SpeedCalculator(float acceleratingTime)
    {
        this.acceleratingTime = acceleratingTime;
    }

    public float CalculateSpeed(float speedMin, float speedMax, out float speedRatio, bool isStopped)
    {
        if (isStopped)
        {
            movingElapsedTime = 0f;
            speedRatio = 0f;
            return 1f;
        }

        movingElapsedTime += Time.deltaTime;
        speedRatio = movingElapsedTime / acceleratingTime;
        speedRatio = speedRatio > 1f ? 1f : speedRatio;
        return Mathf.Lerp(speedMin, speedMax, speedRatio);
    }
}