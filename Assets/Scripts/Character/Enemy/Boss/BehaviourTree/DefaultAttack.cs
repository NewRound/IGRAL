using GlobalEnums;
using System.Collections.Generic;
using UnityEngine;

public class DefaultAttack : ActionNode
{
    private Bullet _bullet;
    private Transform _target;
    private Transform _spawnPoint;
    private int _bulletAmount;

    private BossAnimationController animationController;
    public DefaultAttack(BossBehaviourTree bossBehaviourTree, int amount) : base(bossBehaviourTree)
    {
        _bulletAmount = amount;
        animationController = bossBehaviourTree.AnimationController;
        btDict = bossBehaviourTree.BTDict;
        _bullet = bossBehaviourTree.BulletPrefab;
        _spawnPoint = bossBehaviourTree.BulletSpawnTrans;
    }

    public override NodeState Evaluate()
    {
        if (!IsActionPossible((CurrentAction)btDict[BTValues.CurrentAction], CurrentAction.Attack))
        {
            state = NodeState.Success;
            return state;
        }

        if (!(bool)btDict[BTValues.IsAttacking])
        {
            btDict[BTValues.IsAttacking] = true;
            animationController.PlayAnimation(animationController.AnimationData.AttackSubStateParameterHash, true);
            Shoot();
        }

        float normalizedTime = AnimationUtil.GetNormalizeTime(animationController.Animator, AnimTag.Attack, (int)AnimatorLayer.UpperLayer);
        if (normalizedTime > 1f)
        {
            animationController.PlayAnimation(animationController.AnimationData.AttackSubStateParameterHash, false);
            btDict[BTValues.CurrentAction] = CurrentAction.Patrol;
            btDict[BTValues.IsAttacking] = false;
        }

        state = NodeState.Success;
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

        bossBehaviourTree.LookRightAway();

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
