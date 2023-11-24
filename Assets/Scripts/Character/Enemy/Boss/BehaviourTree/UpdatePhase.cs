using GlobalEnums;

public class UpdatePhase : BossNode
{
    private int _totalPhaseCount;

    public UpdatePhase(BossBehaviourTree bossBehaviourTree) : base(bossBehaviourTree)
    {
    }

    public override NodeState Evaluate()
    {
        int currentPhase = bossBehaviourTree.CurrentPhase;
        currentPhase++;
        currentPhase = currentPhase > _totalPhaseCount ? _totalPhaseCount : currentPhase;
        bossBehaviourTree.SetCurrenPhase(currentPhase);

        state = NodeState.Success;
        return state;
    }
}
