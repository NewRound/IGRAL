using GlobalEnums;
using System.Collections.Generic;

public class BossNode : Node
{
    protected BossBehaviourTree bossBehaviourTree;
    protected Dictionary<BTValues, object> btDict = new Dictionary<BTValues, object>();
    public BossNode(BossBehaviourTree bossBehaviourTree)
    {
        this.bossBehaviourTree = bossBehaviourTree;
        btDict = bossBehaviourTree.BTDict;
    }
}