using GlobalEnums;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Patrol : ActionNode
{
    private Transform[] _waypoints;
    private Rigidbody _rigid;
    private float _currentSpeed;
    private float _speedMin;
    private float _speedMax;

    private Transform _modelTrans;

    private int _currentWayPointIndex;

    private BossAnimationController _animationController;
    private SpeedCalculator _speedCalculator;

    public Patrol(BossBehaviorTree bossBehaviourTree) : base(bossBehaviourTree)
    {
        _animationController = bossBehaviourTree.AnimationController;
        _rigid = bossBehaviourTree.Rigid;
        _speedCalculator = new SpeedCalculator();
        _waypoints = bossBehaviourTree.Waypoints;

        _currentWayPointIndex = _waypoints.Length / 2;

        _speedMin = bossBehaviourTree.StatHandler.Data.SpeedMin;
        _speedMax = bossBehaviourTree.StatHandler.Data.SpeedMax;
        _modelTrans = bossBehaviourTree.ModelTrans;
        btDict = bossBehaviourTree.BTDict;
    }

    public override NodeState Evaluate()
    {
        return GetMoveState();
    }

    private NodeState GetMoveState()
    {
        if (!IsActionPossible((CurrentAction)btDict[BTValues.CurrentAction], CurrentAction.Patrol))
        {
            state = NodeState.Failure;
            return state;
        }

        Vector3 velocity = Vector3.zero;

        float horizontalSub = _rigid.position.x - _waypoints[_currentWayPointIndex].position.x;
        bool isWaypointLeft = horizontalSub > 0;
        velocity = isWaypointLeft ? Vector3.left * _currentSpeed * Time.fixedDeltaTime : Vector3.right * _currentSpeed * Time.fixedDeltaTime;
        velocity.y = _rigid.velocity.y;
        _rigid.velocity = velocity;

        LookRightAway(isWaypointLeft);

        float horizontalDistance = isWaypointLeft ? horizontalSub : -horizontalSub;
        if (horizontalDistance < 0.1f)
        {
            UpdateWayPoint();

            velocity.x = 0f;
            velocity.y = _rigid.velocity.y;
            _rigid.velocity = velocity;

            UpdateMoveAnimation(true);

            btDict[BTValues.CurrentAction] = CurrentAction.UsingSkill;

            state = NodeState.Failure;
            return state;
        }

        UpdateMoveAnimation(false);
        state = NodeState.Success;
        return state;
    }

    private void UpdateWayPoint()
    {
        int random = UnityEngine.Random.Range(0, 2);
        if (random == 0)
        {
            _currentWayPointIndex = 
                _currentWayPointIndex < (_waypoints.Length - 1) ? 
                _currentWayPointIndex + 1: 
                _currentWayPointIndex - 1;
        }
        else
        {
            _currentWayPointIndex =
                _currentWayPointIndex == 0 ?
                _currentWayPointIndex + 1 :
                _currentWayPointIndex - 1;
        }
    }

    private void LookRightAway(bool isWaypointLeft)
    {
        Vector3 direction = isWaypointLeft ? -_rigid.transform.right : _rigid.transform.right;
        _modelTrans.rotation = Quaternion.LookRotation(direction);
    }

    private void UpdateMoveAnimation(bool isStopped)
    {
        _currentSpeed = _speedCalculator.CalculateSpeed(_speedMin, _speedMax, out float speedRatio, isStopped);
        _animationController.PlayAnimation(_animationController.AnimationData.SpeedRatioParameterHash, speedRatio);
    }
}
