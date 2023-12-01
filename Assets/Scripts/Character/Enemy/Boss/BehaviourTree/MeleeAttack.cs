using GlobalEnums;
using UnityEngine;

public class MeleeAttack : ActionNode
{
    private BossAnimationController _animationController;
    private Transform _myTrans;
    private Transform _targetTrans;
    private Vector3 _rayOffsetVec;
    private string _targetLayerName;
    private EnemySO _data;
    private float _meleeAttackMod;
    private StatChange[] _attackStatChange = new StatChange[1];

    public MeleeAttack(BossBehaviourTree bossBehaviourTree) : base(bossBehaviourTree)
    {
        _animationController = bossBehaviourTree.AnimationController;
        btDict = bossBehaviourTree.BTDict;
        _myTrans = bossBehaviourTree.transform;
        _rayOffsetVec = bossBehaviourTree.AttackOffsetVec;
        _targetLayerName = Tag.Player.ToString();
        _data = bossBehaviourTree.StatHandler.Data;
        _meleeAttackMod = bossBehaviourTree.MeleeAttackMod;
        _attackStatChange[0] = new StatChange(StatsChangeType.Subtract, StatType.Health, _data.Attack * _meleeAttackMod);
    }

    public override NodeState Evaluate()
    {
        if (!(bool)btDict[BTValues.IsAttacking])
        {
            btDict[BTValues.IsAttacking] = true;
            _animationController.MeleeAttackAction += Attack;
            bossBehaviourTree.LookRightAway();
            _animationController.PlayAnimation(_animationController.AnimationData.MeleeAttackParameterHash, true);
            _animationController.PlayAnimation(_animationController.AnimationData.AttackSubStateParameterHash, true);
        }

        float normalizedTime = AnimationUtil.GetNormalizeTime(_animationController.Animator, AnimTag.Attack, (int)AnimatorLayer.UpperLayer);
        if (normalizedTime > 1f)
        {
            _animationController.PlayAnimation(_animationController.AnimationData.MeleeAttackParameterHash, false);
            _animationController.PlayAnimation(_animationController.AnimationData.AttackSubStateParameterHash, false);
            _animationController.AttackAction -= Attack;
            btDict[BTValues.CurrentAction] = CurrentAction.Patrol;
            btDict[BTValues.IsAttacking] = false;
        }

        state = NodeState.Success;
        return state;
    }

    private void Attack()
    {
        if (_targetTrans == null)
            _targetTrans = bossBehaviourTree.PlayerTransform;

        Vector3 startPos = _myTrans.position;

        bool isTargetRight = (_targetTrans.position.x - _myTrans.position.x) > 0;
        _rayOffsetVec.x = isTargetRight ? _rayOffsetVec.x : -_rayOffsetVec.x;

        startPos += _rayOffsetVec;

        Vector3 direction = isTargetRight ? _myTrans.right : -_myTrans.right;

        direction *= Mathf.Abs(_rayOffsetVec.x) + _data.AttackRange;

        RaycastHit hit;
        if (Physics.Raycast(startPos, direction, out hit, 1 << LayerMask.NameToLayer(_targetLayerName)))
        {
            PlayerStatHandler statHandler = hit.collider.GetComponent<PlayerController>().StatHandler;
            statHandler.UpdateStats(_attackStatChange);
        }
    }
}