using GlobalEnums;

public class UpdatePhase : BossNode
{
    private int _totalPhaseCount;

    public UpdatePhase(BossBehaviourTree bossBehaviourTree) : base(bossBehaviourTree)
    {
        _totalPhaseCount = bossBehaviourTree.PhaseInfoArr.Length;
    }

    public override NodeState Evaluate()
    {
        if ((bool)btDict[BTValues.IsAttacking])
        {
            state = NodeState.Failure;
            return state;
        }    

        int currentPhase = bossBehaviourTree.CurrentPhase;
        currentPhase++;
        currentPhase = currentPhase > _totalPhaseCount ? _totalPhaseCount : currentPhase;
        bossBehaviourTree.SetCurrenPhase(currentPhase);

        state = NodeState.Success;
        return state;
    }
}
