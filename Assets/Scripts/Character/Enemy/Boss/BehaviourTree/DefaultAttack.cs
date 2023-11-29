using GlobalEnums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DefaultAttack : ActionNode
{
    private Transform _target;
    private Transform _spawnPoint;
    private int _bulletAmount;
    private float _bulletPerAngle;

    private BossAnimationController animationController;
    public DefaultAttack(BossBehaviourTree bossBehaviourTree) : base(bossBehaviourTree)
    {
        _bulletAmount = bossBehaviourTree.BulletCount;
        _bulletPerAngle = bossBehaviourTree.BulletAngle;
        animationController = bossBehaviourTree.AnimationController;
        btDict = bossBehaviourTree.BTDict;
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
            animationController.AttackAction += Shoot;
            bossBehaviourTree.LookRightAway();
            animationController.PlayAnimation(animationController.AnimationData.AttackSubStateParameterHash, true);
        }

        float normalizedTime = AnimationUtil.GetNormalizeTime(animationController.Animator, AnimTag.Attack, (int)AnimatorLayer.UpperLayer);
        if (normalizedTime > 1f)
        {
            animationController.PlayAnimation(animationController.AnimationData.AttackSubStateParameterHash, false);
            animationController.AttackAction -= Shoot;
            btDict[BTValues.CurrentAction] = CurrentAction.Patrol;
            btDict[BTValues.IsAttacking] = false;
        }

        state = NodeState.Success;
        return state;
    }

    public void Shoot()
    {
        if (!_target)
            _target = GameManager.Instance.PlayerTransform;


        for (int i = 0; i < _bulletAmount; i++)
        {
            Vector3 direction = _target.position - _spawnPoint.position;
            Vector3 afterDirection = direction.normalized;

            float modAngle = 0;

            Bullet bullet = ObjectPoolingManager.Instance.GetGameObject(ObjectPoolType.BossShotgunBullet).GetComponent<Bullet>();
            bullet.transform.position = _spawnPoint.position;

            int halfIndex = i / 2;
            if (_bulletAmount % 2 != 0 && i == 0)
                modAngle = 0;
            else
                modAngle = i % 2 == 0 ? -_bulletPerAngle * halfIndex : _bulletPerAngle * (halfIndex + 1);

            if (modAngle != 0)
                afterDirection = Quaternion.Euler(new Vector3(0f, 0f, modAngle)) * direction.normalized;

            bullet.Look(afterDirection);
            bullet.Move();
        }
    }
}
