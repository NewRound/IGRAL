using UnityEngine;

public class RotationCalculator
{
    private float _rotationSpeed;
    private float _minAbsAngle;
    private float _maxAbsAngle;

    public RotationCalculator(float rotationSpeed, float minAbsAngle, float maxAbsAngle)
    {
        _rotationSpeed = rotationSpeed;
        _minAbsAngle = minAbsAngle;
        _maxAbsAngle = maxAbsAngle;
    }

    public float CalculateRotation(float rotationY, Vector3 _preDirection)
    {
        float currentAngle = rotationY;
        float targetAngle = Vector3.SignedAngle(Vector3.forward, _preDirection, Vector3.up);
        Debug.Log($"currentAngle : {currentAngle}\n targetAngle : {targetAngle}");


        float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, _rotationSpeed * Time.deltaTime);


        newAngle = newAngle >= 0 ?
            Mathf.Clamp(Mathf.Abs(newAngle), _minAbsAngle, _maxAbsAngle) :
            -Mathf.Clamp(Mathf.Abs(newAngle), _minAbsAngle, _maxAbsAngle);

        return newAngle;
    }
}