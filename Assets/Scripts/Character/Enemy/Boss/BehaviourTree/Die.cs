using GlobalEnums;
using UnityEngine;

public class Die : BossNode
{
    public Die(BossBehaviourTree bossBehaviourTree) : base(bossBehaviourTree)
    {
    }

    public override NodeState Evaluate()
    {
        Object.Destroy(bossBehaviourTree.gameObject);
        state = NodeState.Success;
        return state;
    }
}