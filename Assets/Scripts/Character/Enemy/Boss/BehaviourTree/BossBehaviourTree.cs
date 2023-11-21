using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviourTree : BehaviourTree
{
    [SerializeField] private Transform[] waypoints;

    [SerializeField] private EnemySO enemySO;

    [SerializeField] private GameObject bulletPrefab;
    
    private Rigidbody _rigid;


    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    protected override Node SetTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new PatrolNode(_rigid, waypoints, enemySO.SpeedMax),
                new ShootNode(bulletPrefab.GetComponent<Bullet>(), GameManager.Instance.PlayerTransform, transform ,5)
            })
        });
            
            

        return root;
    }
}
