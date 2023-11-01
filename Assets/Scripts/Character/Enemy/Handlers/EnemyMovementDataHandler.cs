using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyMovementDataHandler : MovementDataHandler
{
    public bool IsTracing { get; private set; }
    private EnemyMovementData _movementData;
    private Transform _myTrans;
    private float _tileXPos;
    private float _tileHalfLegth;

    private float _targetXPos;

    public EnemyMovementDataHandler(EnemyMovementData movementData, Rigidbody rigidbody, Transform transform, float speedMin, float speedMax) : base(movementData, rigidbody, speedMin, speedMax)
    {
        _movementData = movementData;
        _myTrans = transform;
        // TODO : Å×½ºÆ®
        SetAreaData(9, 5);
    }

    public override void UpdateSpeed()
    {
        base.UpdateSpeed();

        if (direction == Vector2.zero)
            return;

        if (IsTracing)
        {
            speedRatio = speedRatio > _movementData.PatrolAnimationRatio ? 
                _movementData.PatrolAnimationRatio : speedRatio;
        }
    }

    public override void Look()
    {
        if (direction.x == 0)
        {
            return;
        }

        base.Look();
    }

    public void SetIsTracing(bool isTracing)
    {
        IsTracing = isTracing;
    }

    public void SetAreaData(float tileXPos, float tileLegth)
    {
        _tileXPos = tileXPos;
        _tileHalfLegth = tileLegth * 0.5f;
    }

    private void CalculateDirection()
    {
        float randomXPos = UnityEngine.Random.Range(_tileXPos - _tileHalfLegth, _tileXPos + _tileHalfLegth);

        bool isLeft = _myTrans.position.x > randomXPos;

        _targetXPos = randomXPos;

        int directionX = isLeft ? -1 : 1;

        SetDirection(directionX);
    }

    public async void CheckArrivedTargetPos()
    {
        if (IsTracing)
            return;

        CalculateDirection();

        bool isFar = true;

        while (isFar)
        {
            isFar = direction.x < 0 ?
            _targetXPos <= _myTrans.position.x : _targetXPos >= _myTrans.position.x;
            await Task.Delay(_movementData.CheckMilimeterSeconds);
        }

        SetDirection(Vector2.zero);

        await Task.Delay(_movementData.WaitPatrolTime);

        CheckArrivedTargetPos();
    }
}
