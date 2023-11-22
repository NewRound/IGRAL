using GlobalEnums;

public class UpdatePhaseNode : Node
{
    private int _currentPhase = 1;

    private BossBehaviourTree _bossBehaviourTree;

    public UpdatePhaseNode(BossBehaviourTree bossBehaviourTree)
    {
        _bossBehaviourTree = bossBehaviourTree;
    }

    public override NodeState Evaluate()
    {
        if (_currentPhase != _bossBehaviourTree.CurrentPhase)
        {
            // TODO : ��ų ���� ��Ű��

        }

        state = NodeState.Success;
        return state;
    }
}
