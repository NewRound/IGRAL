using GlobalEnums;
using System;

public class UpdatePhase : BossNode
{
    private int _totalPhaseCount;
    

    public UpdatePhase(BossBehaviorTree bossBehaviourTree) : base(bossBehaviourTree)
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

        if (currentPhase == _totalPhaseCount)
        {
            state = NodeState.Failure;
            return state;
        }

        currentPhase++;
        currentPhase = currentPhase > _totalPhaseCount ? _totalPhaseCount : currentPhase;
        bossBehaviourTree.SetCurrenPhase(currentPhase);

        bossBehaviourTree.OnUpdatePhaseUI(currentPhase);
        bossBehaviourTree.OnUpdateCurrentCoolTimeUI(bossBehaviourTree.PhaseInfoArr[currentPhase - 1].SkillCoolTime);

        state = NodeState.Success;
        return state;
    }
}
