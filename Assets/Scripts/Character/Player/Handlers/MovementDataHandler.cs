using System;
using UnityEngine;

public abstract class MovementDataHandler
{
    public Vector3 PreDirection { get; private set; }
    protected Vector2 direction;

    public Rigidbody Rigid;

    [Header("Speed")]
    protected SpeedCalculator speedCalculator;
    protected float speed;
    protected float speedRatio;
    protected float speedMin;
    protected float speedMax;

    [Header("Rotation")]
    protected RotationCalculator rotationCalculator;
    protected Transform modelTrans;

    public MovementDataHandler(MovementData movementData, Rigidbody rigidbody, float speedMin, float speedMax)
    {
        speedCalculator = new SpeedCalculator(movementData.AcceleratingTime);
        rotationCalculator = new RotationCalculator(movementData.RotationSpeed);
        Rigid = rigidbody;
        modelTrans = movementData.ModelTrans;

        this.speedMin = speedMin;
        this.speedMax = speedMax;

        modelTrans.forward = Vector3.right;
        PreDirection = modelTrans.forward;
    }

    public virtual void UpdateSpeed()
    {
        speed = speedCalculator.CalculateSpeed(
            speedMin,
            speedMax,
            out speedRatio,
            direction == Vector2.zero);
    }

    public void Move()
    {
        Vector3 velocity = new Vector3(direction.x, 0f, 0f) * speed;
        velocity.y = Rigid.velocity.y;
        Rigid.velocity = velocity;
    }

    public virtual void Look()
    {
        if (direction.x == 0)
        {
            LookPreDirectionRightAway();
            return;
        }

        PreDirection = direction.x * Vector3.right;

        modelTrans.rotation = rotationCalculator.CalculateRotation(modelTrans.rotation, PreDirection);
    }

    public void LookPreDirectionRightAway()
    {
        modelTrans.rotation = Quaternion.LookRotation(PreDirection);
    }

    public float GetSpeedRatio()
    {
        return speedRatio;
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }

    protected void SetDirection(float xPos)
    {
        SetDirection(new Vector2(xPos, 0f));
    }
}