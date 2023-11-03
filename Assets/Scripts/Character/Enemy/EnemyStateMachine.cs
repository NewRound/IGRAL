using System;
using System.Collections;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public EnemyController EnemyController { get; private set; }

    public EnemyPatrolState PatrolState { get; private set; }
    public EnemyTraceState TraceState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }

    public Transform PlayerTransform { get; private set; }

    public bool IsAttacking { get; private set; }
    public bool IsTracing { get; private set; }

    private float _tileXPos;
    private float _tileHalfLength;
    private float _tilehalfPowLength;
    private float _targetXPos;

    private IEnumerator _currentEnumerator;

    public EnemyStateMachine(EnemyController controller)
    {
        EnemyController = controller;

        SpeedCalculator = new SpeedCalculator(EnemyController.MovementData.AcceleratingTime);
        RotationCalculator = new RotationCalculator(EnemyController.MovementData.RotationSpeed);
        Rigid = EnemyController.Rigidbody;
        ModelTrans = EnemyController.MovementData.ModelTrans;

        speedMin = EnemyController.StatHandler.Data.SpeedMin;
        speedMax = EnemyController.StatHandler.Data.SpeedMax;

        ModelTrans.forward = Vector3.right;
        SetPreDirection(ModelTrans.forward);

        PlayerTransform = GameManager.Instance.player.transform;

        PatrolState = new EnemyPatrolState(this);
        TraceState = new EnemyTraceState(this);
        AttackState = new EnemyAttackState(this);

    }

    public override void Init()
    {
        ChangeState(PatrolState);
    }

    public override void UpdateSpeed()
    {
        base.UpdateSpeed();

        if (Direction == Vector2.zero)
            return;
    }

    public override void Look()
    {
        if (Direction.x == 0)
        {
            return;
        }

        base.Look();
    }

    public override Quaternion GetRotation()
    {
        return RotationCalculator.CalculateRotation(ModelTrans.rotation, PreDirection);
    }

    public void SetAreaData(float tileXPos, float tileLegth)
    {
        _tileXPos = tileXPos;
        Debug.Log(tileLegth);
        _tileHalfLength = tileLegth * 0.5f;
        _tilehalfPowLength = _tileHalfLength * _tileHalfLength;
    }

    public void SetIsTracing(bool isTracing)
    {
        IsTracing = isTracing;
    }

    public void CheckArrived()
    {
        _currentEnumerator = CheckArrivedTargetPos();
        EnemyController.ExcuteCoroutine(_currentEnumerator);
    }

    public void StopCheckingArrived()
    {
        EnemyController.TerminateCoroutine(_currentEnumerator);
    }

    public void TracePlayer()
    {
        Vector3 direction = (PlayerTransform.position - EnemyController.transform.position).normalized;

        if (EnemyController.transform.position.x <= _tileXPos - _tileHalfLength && direction.x < 0)
        {
            return;
        }
        else if (EnemyController.transform.position.x >= _tileXPos + _tileHalfLength && direction.x > 0)
        {
            return;
        }

        SetDirection(direction.x);
    }

    public void CheckAttackRange()
    {
        float distance = Vector3.Distance(EnemyController.transform.position, PlayerTransform.position);

        IsAttacking = EnemyController.StatHandler.Data.AttackDistance >= distance;
    }

    private IEnumerator CheckArrivedTargetPos()
    {
        if (IsTracing)
            yield break;

        CalculateDirection();

        bool isFar = true;

        while (isFar)
        {
            isFar = Direction.x < 0 ?
            _targetXPos <= EnemyController.transform.position.x : _targetXPos >= EnemyController.transform.position.x;
            yield return null;
        }

        SetDirection(Vector2.zero);

        yield return new WaitForSeconds(EnemyController.MovementData.WaitPatrolTime);

        CheckArrived();
    }

    private void CalculateDirection()
    {
        float randomXPos = UnityEngine.Random.Range(_tileXPos - _tilehalfPowLength, _tileXPos + _tilehalfPowLength);

        randomXPos = Mathf.Clamp(randomXPos, _tileXPos - _tileHalfLength, _tileXPos + _tileHalfLength);

        bool isLeft = EnemyController.transform.position.x > randomXPos;

        _targetXPos = randomXPos;

        int directionX = isLeft ? -1 : 1;

        SetDirection(directionX);
    }

    
}
