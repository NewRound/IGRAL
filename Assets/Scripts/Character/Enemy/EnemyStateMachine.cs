using System.Collections;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public EnemyController EnemyController { get; private set; }

    public EnemyPatrolState PatrolState { get; private set; }
    public EnemyTraceState TraceState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }
    public EnemyDieState DieState { get; private set; }

    public Transform PlayerTransform { get; private set; }

    public bool IsTracing { get; private set; }

    private float _tileXPos;
    private float _tileHalfLength;
    private float _tilehalfPowLength;
    private float _targetXPos;
    private bool _isTargetSet;
    
    private IEnumerator _currentEnumerator;

    public PlayerStateMachine PlayerStateMachine { get; private set; }
    

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

        PlayerTransform = GameManager.Instance.PlayerTransform;
        PlayerStateMachine = GameManager.Instance.PlayerInputController.StateMachine;

        PatrolState = new EnemyPatrolState(this);
        TraceState = new EnemyTraceState(this);
        if(controller.StatHandler.Data.IsRanged)
            AttackState = new EnemyRangedAttackState(this);
        else
            AttackState = new EnemyAttackState(this);
        DieState = new EnemyDieState(this);


    }

    public override void Update()
    {
        base.Update();
    }

    public override void Init()
    {
        ChangeState(PatrolState);
    }

    public override void UpdateSpeed()
    {
        Speed = SpeedCalculator.CalculateSpeed(speedMin, speedMax, out speedRatio, Direction == Vector3.zero, CurrentState == PatrolState);
    }

    public override void Look()
    {
        if (Direction.x == 0)
        {
            LookPreDirectionRightAway();
            return;
        }
        base.Look();
    }

    public override Quaternion GetRotation()
    {
        return RotationCalculator.CalculateRotation(ModelTrans.rotation, PreDirection);
    }

    public override void Move()
    {
        if (IsAttacking)
        {
            SpeedRatio = 0;
            SetDirection(0);
            EnemyController.AnimationController.PlayAnimation(EnemyController.AnimationController.AnimationData.SpeedRatioParameterHash, SpeedRatio);
        }


        base.Move();
    }

    public void SetAreaData(float tileXPos, float tileLegth)
    {
        _tileXPos = tileXPos;
        _tileHalfLength = tileLegth * 0.5f;
        _tilehalfPowLength = _tileHalfLength * _tileHalfLength;
    }

    public void SetIsTracing(bool isTracing)
    {
        IsTracing = isTracing;
    }

    public void CheckArrived()
    {
        if (_isTargetSet)
        {
            _currentEnumerator = CheckArrivedTargetPos();
            EnemyController.ExcuteCoroutine(_currentEnumerator);
        }
    }

    public void SetIsTarget(bool isTargetSet)
    {
        _isTargetSet = isTargetSet;
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

        bool isAttacking = EnemyController.StatHandler.Data.AttackDistance >= distance;
        SetIsAttacking(isAttacking);
    }

    public void Ondead()
    {
        ChangeState(DieState);
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
