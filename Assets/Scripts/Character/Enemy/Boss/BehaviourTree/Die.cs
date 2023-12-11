using GlobalEnums;
using UnityEngine;

public class Die : BossNode
{
    private BossBehaviorTree _bossBehaviourTree;

    public Die(BossBehaviorTree bossBehaviourTree) : base(bossBehaviourTree)
    {
        _bossBehaviourTree = bossBehaviourTree;
    }

    public override NodeState Evaluate()
    {
        _bossBehaviourTree.OnCloseBossUI();
        Object.Destroy(bossBehaviourTree.gameObject);
        state = NodeState.Success;
        return state;
    }
}