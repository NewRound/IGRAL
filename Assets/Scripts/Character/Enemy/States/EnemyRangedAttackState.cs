using UnityEngine;

public class EnemyRangedAttackState : EnemyAttackState
{
    private float _attackDelay;
    private float _attackTime;
    private bool _isAttack = true;

    public EnemyRangedAttackState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
        _attackTime = 0;
        _attackDelay = enemyController.StatHandler.Data.AttackDelay;
    }

    public override void Enter()
    {
        base.Enter();

        animationController.AttackAction += EnemyBulletSpawn;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _attackTime += Time.deltaTime;
        if(_attackDelay <= _attackTime)
        {
            _isAttack = true;
        }
    }

    public override void Exit()
    {
        base.Exit();
        animationController.AttackAction -= EnemyBulletSpawn;
    }


    private void EnemyBulletSpawn()
    {
        if(_isAttack)
        {
            enemyController.EnemyBulletSpawn();
            _isAttack = false;
            _attackTime = 0;
        }
    }

}
