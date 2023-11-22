using GlobalEnums;
using System.Collections.Generic;
using UnityEngine;

public class PatrolNode : Node
{
    private Transform[] _waypoints;
    private Rigidbody _rigid;
    private float _currentSpeed;
    private float _speedMin;
    private float _speedMax;

    private Transform _modelTrans;

    private int _currentWayPointIndex = -1;

    private BossAnimationController _animationController;
    private SpeedCalculator _speedCalculator;

    private Dictionary<BTValues, object> _btDict = new Dictionary<BTValues, object>();

    public PatrolNode(BossBehaviourTree bossBehaviourTree, Rigidbody rigid, Transform[] waypoints)
    {
        _animationController = bossBehaviourTree.AnimationController;
        _rigid = rigid;
        _waypoints = new Transform[waypoints.Length];
        _speedCalculator = new SpeedCalculator();
        _waypoints = waypoints;
        _speedMin = bossBehaviourTree.StatHandler.Data.SpeedMin;
        _speedMax = bossBehaviourTree.StatHandler.Data.SpeedMax;
        _modelTrans = bossBehaviourTree.ModelTrans;
        _btDict = bossBehaviourTree.BTDict;
    }

    public override NodeState Evaluate()
    {
        return GetMoveState();
    }

    private NodeState GetMoveState()
    {
        if (_currentWayPointIndex == -1)
            _currentWayPointIndex = UnityEngine.Random.Range(0, _waypoints.Length);

        if ((bool)_btDict[BTValues.IsAnyActionPlaying])
        {
            state = NodeState.Success;
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
            _currentWayPointIndex = -1;
            
            velocity.x = 0f;
            velocity.y = _rigid.velocity.y;
            _rigid.velocity = velocity;

            UpdateMoveAnimation(true);

            state = NodeState.Success;
            return state;
        }

        UpdateMoveAnimation(false);
        state = NodeState.Running;
        return state;
    }

    private void LookRightAway(bool isWaypointLeft)
    {
        Vector3 direction = isWaypointLeft ? -_rigid.transform.right : _rigid.transform.right;
        _modelTrans.rotation = Quaternion.LookRotation(direction);
    }

    private void UpdateMoveAnimation(bool isStopped)
    {
        if (isStopped)
        {
            _currentSpeed = 0f;
            _animationController.PlayAnimation(_animationController.AnimationData.SpeedRatioParameterHash, 0f);
            return;
        }

        _currentSpeed = _speedCalculator.CalculateSpeed(_speedMin, _speedMax, out float speedRatio, false);
        _animationController.PlayAnimation(_animationController.AnimationData.SpeedRatioParameterHash, speedRatio);
    }
}
