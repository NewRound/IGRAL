using GlobalEnums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviourTree : BehaviourTree
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private EnemySO enemySO;
    [field: SerializeField] public Transform ModelTrans { get; private set; }

    [field: SerializeField] public Transform BulletSpawnTrans { get; private set; }
    [field: SerializeField] public GameObject DefaultWeapon { get; private set; }
    [field: SerializeField] public Bullet BulletPrefab { get; private set; }
    [field: SerializeField] public int BulletCount { get; private set; } = 5;
    [field: SerializeField] public float BulletAngle { get; private set; } = 5f;

    public BossStatHandler StatHandler { get; private set; }

    public BossAnimationController AnimationController { get; private set; }

    [field: SerializeField] public PhaseInfo[] PhaseInfoArr { get; private set; }

    private Rigidbody _rigid;

    public int CurrentPhase { get; private set; } = 1;

    public Dictionary<BTValues, object> BTDict { get; private set; } = new Dictionary<BTValues, object>();

    public Transform PlayerTransform { get; private set; }

    public Transform DroneSpawnTrans { get; private set; }
    [field: SerializeField] public float DroneSpawnDuration { get; private set; } = 3f;
    [field: SerializeField] public float DroneHeight { get; private set; } = 7f;

    public UIBossCondition UIBossCondition { get; private set; }

    [field: Header("UI")]
    public event Action<int> OnUpdatePhase;
    public event Action<float> OnUpdateElapsedCooltime;
    public event Action<float> OnUpdateCooltime;
    public event Action OnBossDead;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        StatHandler = new BossStatHandler(Instantiate(enemySO), this);
        DroneSpawnTrans = waypoints[0];
        AnimationController = GetComponentInChildren<BossAnimationController>();
        AnimationController.Init();
    }

    public void Init()
    {
        if (UIBossCondition == null)
            UIBossCondition = UIManager.Instance.OpenUI<UIBossCondition>();

        UIBossCondition.DisplayHP(enemySO.Health);
        UIBossCondition.SetMaxAction(enemySO.MaxHealth);
        OnUpdateElapsedCooltime += UpdateElapsedCoolTimeUI;
        OnUpdateCooltime += UpdateCurrentCoolTimeUI;
        OnUpdatePhase += UpdatePhaseUI;
        OnBossDead += CloseBossUI;
    }

    protected override Node SetTree()
    {
        InitBTDict();

        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>()
            {
                new CheckNextPhaseHP(this),
                new UpdatePhase(this),
            }),

            new Sequence(new List<Node>()
            {
                new CheckDie(this),
                new Die(this),
            }),

            new Sequence(new List<Node>()
            {
                new Selector(new List<Node>()
                {
                    new Sequence(new List<Node>()
                    {
                        new Patrol(this, _rigid, waypoints),
                        new RunningCoolTime(this),
                    }),

                    new Selector(new List<Node>()
                    {
                        new Boss3Phase1(this),

                        new Boss3Phase2(this),

                        new Boss3Phase3(this)
                    }),

                    new Sequence(new List<Node>()
                    {
                        new DefaultAttack(this),
                        new RunningCoolTime(this),
                    })
                })
            }),
        });

        return root;
    }

    public void SetCurrenPhase(int currentPhase)
    {
        CurrentPhase = currentPhase;
    }

    public void LookRightAway()
    {
        if (!PlayerTransform)
            PlayerTransform = GameManager.Instance.PlayerTransform;

        Vector3 direction = PlayerTransform.position - transform.position;
        direction = direction.x > 0 ? Vector3.right : Vector3.left;
        ModelTrans.rotation = Quaternion.LookRotation(direction);
    }

    public void UpdatePhaseUI(int phase)
    {
        UIBossCondition.ChangePhase(phase);
    }

    private void InitBTDict()
    {
        if (!BTDict.ContainsKey(BTValues.CurrentPhaseSkillCoolTime))
            BTDict.Add(BTValues.CurrentPhaseSkillCoolTime, 0f);

        if (!BTDict.ContainsKey(BTValues.CurrentSkillElapsedTime))
            BTDict.Add(BTValues.CurrentSkillElapsedTime, 0f);

        if (!BTDict.ContainsKey(BTValues.CurrentAction))
            BTDict.Add(BTValues.CurrentAction, CurrentAction.Patrol);

        if (!BTDict.ContainsKey(BTValues.IsAttacking))
            BTDict.Add(BTValues.IsAttacking, false);
    }

    public void UpdateElapsedCoolTimeUI(float elapsedCooltime)
    {
        OnUpdateElapsedCooltime.Invoke(elapsedCooltime);
    }

    public void UpdateCurrentCoolTimeUI(float elapsedCooltime)
    {
        UIBossCondition.SetMaxAction(elapsedCooltime);
    }

    public void UpdateCurrentPhaseUI(int phase)
    {
        UIBossCondition.ChangePhase(phase);
    }

    public void CloseBossUI()
    {
        UIBossCondition.gameObject.SetActive(false);
    }
}
