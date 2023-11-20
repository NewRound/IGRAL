using GlobalEnums;
using UnityEngine;

public class PatrolNode : Node
{
    private Transform[] _waypoints;
    private Rigidbody _rigid;
    private float _speed;

    private int _currentWayPointIndex = -1;

    public PatrolNode(Rigidbody rigid, Transform[] waypoints, float speed)
    {
        _rigid = rigid;
        _waypoints = new Transform[waypoints.Length];
        _waypoints = waypoints;
        _speed = speed;
    }

    public override NodeState Evaluate()
    {
        if (_currentWayPointIndex == -1)
            _currentWayPointIndex = UnityEngine.Random.Range(0, _waypoints.Length);

        float horizontalSub = _rigid.position.x - _waypoints[_currentWayPointIndex].position.x;
        bool isWaypointLeft = horizontalSub > 0;
        Vector3 velocity = isWaypointLeft ? Vector3.left * _speed : Vector3.right * _speed;
        velocity.y = _rigid.velocity.y;
        _rigid.velocity = velocity;

        float horizontalDistance = isWaypointLeft ? horizontalSub : -horizontalSub;
        if (horizontalDistance < 0.01f)
        {
            _currentWayPointIndex = -1;
            state = NodeState.Success;
            return state;
        }

        state = NodeState.Running;
        return state;
    }
}
