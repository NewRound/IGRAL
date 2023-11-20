using GlobalEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    protected NodeState state;

    public Node Parent { get; private set; }
    protected List<Node> children = new List<Node>();

    public Node()
    {
        Parent = null;
    }

    public Node(List<Node> children)
    {
        foreach (Node child in children)
            SetNode(child);
    }

    private void SetNode(Node node)
    {
        node.Parent = this;
        children.Add(node);
    }

    public virtual NodeState Evaluate() => NodeState.Failure;


}
