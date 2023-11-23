using GlobalEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
    public Selector() : base()
    {

    }

    public Selector(List<Node> children) : base(children)
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
                    state = NodeState.Success;
                    return state;
                case NodeState.Failure:
                    continue;
            }
        }

        state = NodeState.Failure;
        return state;
    }
}
