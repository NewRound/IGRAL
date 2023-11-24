using GlobalEnums;
public class ActionNode : BossNode
{
    public ActionNode(BossBehaviourTree bossBehaviourTree) : base(bossBehaviourTree)
    {
    }

    protected bool IsActionPossible(CurrentAction action, CurrentAction comparingAction)
    {
        if (action == CurrentAction.None || action == comparingAction)
            return true;

        return false;
}