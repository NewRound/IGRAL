using GlobalEnums;
using UnityEngine;

public class Die : BossNode
{
    private BossBehaviorTree _bossBehaviourTree;
    private bool _isDead;

    public Die(BossBehaviorTree bossBehaviourTree) : base(bossBehaviourTree)
    {
        _bossBehaviourTree = bossBehaviourTree;
    }

    public override NodeState Evaluate()
    {
        if (_isDead)
        {
            state = NodeState.Running;
            return state;
        }

        _isDead = true;
        _bossBehaviourTree.OnCloseBossUI();

        BossAnimationController animationController = _bossBehaviourTree.AnimationController;
        animationController.PlayAnimation(animationController.AnimationData.DieParameterHash, true);


        Object.Destroy(bossBehaviourTree.gameObject, _bossBehaviourTree.DieDuration);
        state = NodeState.Success;
        return state;
    }
}