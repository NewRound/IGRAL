using UnityEngine;

public class EnemyDieState : EnemyStateBase
{
    public EnemyDieState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {

    }

    public override void Enter()
    {
        if (stateMachine.IsDead)
            return;

        OnDead();
    }

    public override void UpdateState()
    {
        if (!stateMachine.IsDead)
            stateMachine.ChangeState(stateMachine.PatrolState);
    }

    public override void Exit()
    {
        stateMachine.SetDead(false);
        animationController.PlayAnimation(animationsData.dieParameterHash, false);
    }

    public override void OnDead()
    {
        base.OnDead();
        stateMachine.SetDead(true);
        stateMachine.SetDirection(Vector2.zero);
        animationController.PlayAnimation(animationsData.dieParameterHash, true);
    }
}
