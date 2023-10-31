using UnityEngine;

public class RotationCalculator
{
    private float _rotationSpeed;
    private const int CIRCLE_ANGLE = 360;

    public RotationCalculator(float rotationSpeed)
    {
        _rotationSpeed = rotationSpeed;
    }

    public Quaternion CalculateRotation(Quaternion rotation, Vector3 _preDirection)
    {
        float targetAngle = Vector3.SignedAngle(Vector3.forward, _preDirection, Vector3.up);

        float rotationY = rotation.eulerAngles.y;

        rotationY = targetAngle < 0 ? rotationY++ : rotationY--;

        if (targetAngle < 0)
            targetAngle += CIRCLE_ANGLE;

        Quaternion startRotation = Quaternion.Euler(new Vector3(rotation.x, rotationY, rotation.z));
        Quaternion targetRotation = Quaternion.Euler(new Vector3(rotation.x, targetAngle, rotation.z));

        Quaternion newRotation = Quaternion.RotateTowards(startRotation, targetRotation, _rotationSpeed);

        return newRotation;
    }
}