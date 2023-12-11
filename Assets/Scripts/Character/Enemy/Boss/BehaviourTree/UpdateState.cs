using GlobalEnums;
using System.Collections.Generic;

public class UpdateState : BossNode
{
    public UpdateState(BossBehaviorTree bossBehaviourTree) : base(bossBehaviourTree)
    {
    }

    public override NodeState Evaluate()
    {
        CurrentAction currentAction = (CurrentAction)btDict[BTValues.CurrentAction];

        switch (currentAction)
        {
            case CurrentAction.Patrol:
                btDict[BTValues.CurrentAction] = CurrentAction.UsingSkill;
                break;
            case CurrentAction.RangedAttack:
                btDict[BTValues.CurrentAction] = CurrentAction.Patrol;
                break;
            case CurrentAction.UsingSkill:
                btDict[BTValues.CurrentAction] = CurrentAction.Patrol;
                break;
        }

        state = NodeState.Success;
        return state;
    }
}