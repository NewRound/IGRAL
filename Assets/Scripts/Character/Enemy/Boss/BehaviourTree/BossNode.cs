using GlobalEnums;
using System.Collections.Generic;

public class BossNode : Node
{
    protected BossBehaviorTree bossBehaviourTree;
    protected Dictionary<BTValues, object> btDict = new Dictionary<BTValues, object>();
    public BossNode(BossBehaviorTree bossBehaviourTree)
    {
        this.bossBehaviourTree = bossBehaviourTree;
        btDict = bossBehaviourTree.BTDict;
    }
}