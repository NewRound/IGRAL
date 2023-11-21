using GlobalEnums;
using System.Collections.Generic;

public class Sequence : Node
{
    public Sequence() : base() 
    { 
    
    }
    
    public Sequence(List<Node> children) : base(children) 
    { 
    
    }

    public override NodeState Evaluate()
    {
        foreach (Node node in children)
        {
            switch (node.Evaluate())
            {
                case NodeState.Running:
                    state = NodeState.Running;
                    return state;
                case NodeState.Success:
                    continue;
                case NodeState.Failure:
                    state = NodeState.Failure;
                    return state;
            }
        }

        state = NodeState.Success;
        return state;
    }
}
