using GlobalEnums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviorTree : BehaviorTree
{
    [SerializeField] private EnemySO enemySO;
    public Transform[] Waypoints { get; private set; }
    [field: SerializeField] public Transform ModelTrans { get; private set; }

    [field: SerializeField] public Transform BulletSpawnTrans { get; private set; }
    [field: SerializeField] public GameObject DefaultWeapon { get; private set; }
    [field: SerializeField] public Bullet BulletPrefab { get; private set; }
    [field: SerializeField] public int BulletCount { get; private set; } = 5;
    [field: SerializeField] public float BulletAngle { get; private set; } = 5f;

    public BossStatHandler StatHandler { get; private set; }

    public BossAnimationController AnimationController { get; private set; }

    [field: SerializeField] public PhaseInfo[] PhaseInfoArr { get; private set; }

    [field: SerializeField] public Vector3 AttackOffsetVec = new Vector3(-0.2f, 0.5f, 0f);
    [field: SerializeField] public float MeleeAttackMod = 2.5f;


    public Rigidbody Rigid { get; private set; }

    public int CurrentPhase { get; private set; } = 1;

    public Dictionary<BTValues, object> BTDict { get; private set; } = new Dictionary<BTValues, object>();

    public Transform PlayerTransform { get; private set; }

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
        Rigid = GetComponent<Rigidbody>();
        StatHandler = new BossStatHandler(Instantiate(enemySO), this);
        AnimationController = GetComponentInChildren<BossAnimationController>();
        AnimationController.Init();
    }

    private void OnDestroy()
    {
        OnUpdateElapsedCooltime -= UpdateElapsedCoolTimeUI;
        OnUpdateCooltime -= UpdateCurrentCoolTimeUI;
        OnUpdatePhase -= UpdatePhaseUI;
        OnBossDead -= CloseBossUI;
    }

    public void Init(Transform[] waypoints)
    {
        Waypoints = waypoints;

        Init();
    }

    protected override void Init()
    {
        if (UIBossCondition == null)
        {
            PlayerTransform = GameManager.Instance.PlayerTransform;
            UIBossCondition = UIManager.Instance.OpenUI<UIBossCondition>();
            InitBTDict();
            StatHandler.Init();
            OnUpdateElapsedCooltime += UpdateElapsedCoolTimeUI;
            OnUpdateCooltime += UpdateCurrentCoolTimeUI;
            OnUpdatePhase += UpdatePhaseUI;
            OnBossDead += CloseBossUI;
            UIBossCondition.SetMaxAction(PhaseInfoArr[CurrentPhase - 1].SkillCoolTime);
        }

        base.Init();
    }

    protected override Node SetTree()
    {
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
                        new Patrol(this),
                        new RunningCoolTime(this),
                    }),

                    new Selector(new List<Node>()
                    {
                        new BossPhase1(this),

                        new BossPhase2(this),

                        new BossPhase3(this)
                    }),

                    new Selector(new List<Node>()
                    {
                        new Sequence(new List<Node>()
                        {
                            new CheckTargetDistance(this),
                            new MeleeAttack(this),
                            new RunningCoolTime(this),
                        }),

                        new Sequence(new List<Node>()
                        {
                            new DefaultAttack(this),
                            new RunningCoolTime(this),
                        })
                    }),
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

    public void OnUpdateElapsedCoolTimeUI(float elapsedCooltime)
    {
        OnUpdateElapsedCooltime.Invoke(elapsedCooltime);
    }

    public void OnUpdateCurrentCoolTimeUI(float elapsedCooltime)
    {
        OnUpdateCooltime.Invoke(elapsedCooltime);
    }

    public void OnUpdatePhaseUI(int phase)
    {
        OnUpdatePhase.Invoke(phase);
    }

    public void OnCloseBossUI()
    {
        OnBossDead.Invoke();
    }

    private void UpdateElapsedCoolTimeUI(float elapsedCooltime)
    {
        UIBossCondition.DisplayAction(elapsedCooltime);
    }

    private void UpdateCurrentCoolTimeUI(float elapsedCooltime)
    {
        UIBossCondition.SetMaxAction(elapsedCooltime);
    }

    private void UpdatePhaseUI(int phase)
    {
        UIBossCondition.ChangePhase(phase);
    }

    private void CloseBossUI()
    {
        UIBossCondition.gameObject.SetActive(false);
    }
}
