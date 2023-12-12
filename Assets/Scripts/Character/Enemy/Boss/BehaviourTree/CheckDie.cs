using GlobalEnums;

public class CheckDie : BossNode
{
    public CheckDie(BossBehaviorTree bossBehaviourTree) : base(bossBehaviourTree)
    {
    }

    public override NodeState Evaluate()
    {
        if (bossBehaviourTree.StatHandler.Data.Health <= 0)
        {
            state = NodeState.Success;
            return state;
        }

        state = NodeState.Failure;
        return state;
    }
}