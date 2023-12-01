using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourTree : MonoBehaviour
{
    private Node _rootNode;

    protected virtual void Init()
    {
        _rootNode = SetTree();
    }

    private void Update()
    {
        if (_rootNode != null)
            _rootNode.Evaluate();
    }

    protected abstract Node SetTree();
}
