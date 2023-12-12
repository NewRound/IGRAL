using GlobalEnums;

public class BossPhase3 : BossSkill
{
    private EnemyDroneSpawner _enemyDroneSpawner;

    public BossPhase3(BossBehaviorTree bossBehaviourTree) : base(bossBehaviourTree)
    {
        _enemyDroneSpawner = new EnemyDroneSpawner(bossBehaviourTree.Waypoints, bossBehaviourTree.DroneSpawnDuration, bossBehaviourTree.DroneHeight);
    }

    public override NodeState Evaluate()
    {
        if (!IsActionPossible((CurrentAction)btDict[BTValues.CurrentAction], CurrentAction.UsingSkill)
            || bossBehaviourTree.CurrentPhase != 3)
        {
            state = NodeState.Failure;
            return state;
        }

        if ((bool)btDict[BTValues.IsAttacking])
        {
            float normalizedTime = AnimationUtil.GetNormalizeTime(animationController.Animator, AnimTag.Skill, (int)AnimatorLayer.UpperLayer);

            if (normalizedTime > 1f)
            {
                OnAnimationEnded();
                _enemyDroneSpawner.SpawnDrone();
                state = NodeState.Success;
                return state;
            }

            state = NodeState.Running;
            return state;
        }


        if ((float)btDict[BTValues.CurrentSkillElapsedTime] >= (float)btDict[BTValues.CurrentPhaseSkillCoolTime])
        {
            OnChargedCoolTime();
            state = NodeState.Success;
            return state;
        }

        btDict[BTValues.CurrentAction] = CurrentAction.RangedAttack;
        state = NodeState.Failure;
        return state;
    }

    protected override void OnChargedCoolTime()
    {
        base.OnChargedCoolTime();
        UseSkill();
    }

    protected override void Init()
    {
        
    }
}