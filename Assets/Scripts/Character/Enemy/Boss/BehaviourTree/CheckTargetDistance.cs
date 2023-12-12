using GlobalEnums;
using UnityEngine;

public class CheckTargetDistance : ActionNode
{
    private Transform _target;
    private Transform _myTrans;
    private float _attackRange;

    public CheckTargetDistance(BossBehaviorTree bossBehaviourTree) : base(bossBehaviourTree)
    {
        _myTrans = bossBehaviourTree.transform;
        _attackRange = bossBehaviourTree.StatHandler.Data.AttackRange + Mathf.Abs(bossBehaviourTree.AttackOffsetVec.x);
        _target = bossBehaviourTree.PlayerTransform;
    }

    public override NodeState Evaluate()
    {
        if ((bool)btDict[BTValues.IsAttacking])
        {
            CurrentAction currentAction = (CurrentAction)btDict[BTValues.CurrentAction];

            if (IsActionPossible(currentAction, CurrentAction.MeleeAttack))
            {
                state = NodeState.Success;
                return state;
            }
            else if (IsActionPossible(currentAction, CurrentAction.RangedAttack))
            {
                state = NodeState.Failure;
                return state;
            }
        }
        else
        {
            if (_target == null)
                _target = bossBehaviourTree.PlayerTransform;

            float distance = Vector3.Distance(_target.position, _myTrans.position);
            if (distance <= _attackRange)
            {
                btDict[BTValues.CurrentAction] = CurrentAction.MeleeAttack;
                state = NodeState.Success;
                return state;
            }
        }

        state = NodeState.Failure;
        return state;
    }
}