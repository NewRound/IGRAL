using GlobalEnums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviourTree : BehaviourTree
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private EnemySO enemySO;

    public EnemyStatHandler StatHandler { get; private set; }

    [SerializeField] private Transform bulletSpawnTrans;
    [SerializeField] private Bullet bulletPrefab;

    [field: SerializeField] public PhaseSO PhaseSO { get; private set; }

    private Rigidbody _rigid;

    public int CurrentPhase { get; private set; } = 1;

    public Dictionary<BTValues, object> BTDict { get; private set; } = new Dictionary<BTValues, object>();


    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        StatHandler = new EnemyStatHandler(enemySO, null, null,transform);
    }

    protected override Node SetTree()
    {
        InitBTDict();

        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new RunningCoolTimeNode(this),
                new CheckHpNode(this, PhaseSO.PhaseInfo.Length),
                new UpdatePhaseNode(this),
                new PatrolNode(_rigid, waypoints, StatHandler.Data.SpeedMax),
                new Selector(new List<Node>
                {
                    new Sequence(new List<Node>
                    {
                        new CheckSkillCoolTimeNode(this),
                        new UseSkillNode(this)
                    }),
                    new ShootNode(bulletPrefab, bulletSpawnTrans, GameManager.Instance.PlayerTransform ,5)
                }),
            }),
            new DieNode()
        });

        return root;
    }

    private void InitBTDict()
    {
        if (!BTDict.ContainsKey(BTValues.CurrentPhaseSkillCoolTime))
            BTDict.Add(BTValues.CurrentPhaseSkillCoolTime, 0f);

        if (!BTDict.ContainsKey(BTValues.WasSkillUsed))
            BTDict.Add(BTValues.WasSkillUsed, false);

        if (!BTDict.ContainsKey(BTValues.CurrentSkillElapsedTime))
            BTDict.Add(BTValues.CurrentSkillElapsedTime, 0f);
    }

    public void SetCurrenPhase(int currentPhase)
    {
        CurrentPhase = currentPhase;
    }
}
