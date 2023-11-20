using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviourTree : BehaviourTree
{
    [SerializeField] private Transform[] waypoints;

    [SerializeField] private EnemySO _enemySO;

    private Rigidbody _rigid;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    protected override Node SetTree()
    {
        Node root = new PatrolNode(_rigid, waypoints, _enemySO.SpeedMax);

        return root;
    }
}
