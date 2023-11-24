using GlobalEnums;
using System.Collections.Generic;

public class CheckAttackPossible : BossNode
{
    private BossAnimationController _animationController;
    private Dictionary<BTValues, object> _btDict = new Dictionary<BTValues, object>();

    public CheckAttackPossible(BossBehaviourTree bossBehaviourTree) : base(bossBehaviourTree)
    {
        _animationController = bossBehaviourTree.AnimationController;
        _btDict = bossBehaviourTree.BTDict;
    }

    public override NodeState Evaluate()
    {
        state = NodeState.Running; 
        return state;
    }
}