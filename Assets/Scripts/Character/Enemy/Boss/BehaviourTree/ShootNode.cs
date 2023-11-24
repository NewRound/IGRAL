using GlobalEnums;
using System.Collections.Generic;
using UnityEngine;

public class ShootNode : Node
{
    private Bullet _bullet;
    private Transform _target;
    private Transform _spawnPoint;
    private Transform _myTrans;
    private Transform _modelTrans;
    private int _bulletAmount;

    private BossBehaviourTree _bossBehaviourTree;
    private BossAnimationController _animationController;
    private Dictionary<BTValues, object> _btDict = new Dictionary<BTValues, object>();

    public ShootNode(BossBehaviourTree bossBehaviourTree, int amount)
    {
        _bossBehaviourTree = bossBehaviourTree;
        _bulletAmount = amount;

        _animationController = _bossBehaviourTree.AnimationController;
        _btDict = _bossBehaviourTree.BTDict;
        _bullet = _bossBehaviourTree.BulletPrefab;
        _spawnPoint = _bossBehaviourTree.BulletSpawnTrans;

        _myTrans = _bossBehaviourTree.transform;
        _modelTrans = _bossBehaviourTree.ModelTrans;
    }

    public override NodeState Evaluate()
    {
        _btDict[BTValues.IsAnyActionPlaying] = true;
        _animationController.PlayAnimation(_animationController.AnimationData.AttackSubStateParameterHash, true);

        Shoot();

        state = NodeState.Running;
        return state;
    }

    

    private void Shoot()
    {
        if (!_target)
            _target = GameManager.Instance.PlayerTransform;

        float angle = 15;
        float modAngle = 0;
        Vector3 direction = _target.position - _spawnPoint.position;

        bool isRight = direction.x > 0;

        _bossBehaviourTree.LookRightAway();

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
