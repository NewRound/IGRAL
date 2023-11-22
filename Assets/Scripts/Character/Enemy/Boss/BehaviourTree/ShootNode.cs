using GlobalEnums;
using System.Collections.Generic;
using UnityEngine;

public class ShootNode : Node
{
    private Bullet _bullet;
    private Transform _target;
    private Transform _spawnPoint;
    private int _bulletAmount;

    private BossBehaviourTree _bossBehaviourTree;
    private BossAnimationController _animationController;
    private Dictionary<BTValues, object> _btDict = new Dictionary<BTValues, object>();

    public ShootNode(BossBehaviourTree bossBehaviourTree, Transform target, int amount)
    {
        _bossBehaviourTree = bossBehaviourTree;
        _target = target;
        _bulletAmount = amount;

        _animationController = _bossBehaviourTree.AnimationController;
        _btDict = _bossBehaviourTree.BTDict;
        _bullet = _bossBehaviourTree.BulletPrefab;
        _spawnPoint = _bossBehaviourTree.BulletSpawnTrans;

    }

    public override NodeState Evaluate()
    {
        if ((bool)_btDict[BTValues.IsAnyActionPlaying] && !(bool)_btDict[BTValues.WasSkillUsed])
        {
            return GetStateWhileAttacking();
        }

        _btDict[BTValues.IsAnyActionPlaying] = true;
        _animationController.PlayAnimation(_animationController.AnimationData.AttackSubStateParameterHash, true);

        Shoot();

        state = NodeState.Running;
        return state;
    }

    private NodeState GetStateWhileAttacking()
    {
        float normalizedTime = AnimationUtil.GetNormalizeTime(_animationController.Animator, AnimTag.Action, (int)AnimatorLayer.UpperLayer);
        if (normalizedTime > 1f)
        {
            _btDict[BTValues.IsAnyActionPlaying] = false;
            _animationController.PlayAnimation(_animationController.AnimationData.AttackSubStateParameterHash, false);
            state = NodeState.Success;
            return state;
        }
        else
        {
            state = NodeState.Running;
            return state;
        }
    }

    private void Shoot()
    {
        float angle = 15;
        float modAngle = 0;
        Vector3 direction = _target.position - _spawnPoint.position;

        bool isRight = direction.x > 0;

        for (int i = 0; i < _bulletAmount; i++)
        {
            int halfIndex = i / 2;
            if (_bulletAmount % 2 != 0 && i == 0)
                modAngle = 0;
            else
                modAngle = i % 2 == 0 ? -angle * (halfIndex + 1) : angle * (halfIndex + 1);

            Vector3 afterDirection = Quaternion.Euler(direction.normalized) * new Vector3(0, 0, modAngle);

            Bullet bullet = Object.Instantiate(_bullet, _spawnPoint.position, Quaternion.Euler(afterDirection));
            bullet.Move(isRight);
        }
    }

    
}
