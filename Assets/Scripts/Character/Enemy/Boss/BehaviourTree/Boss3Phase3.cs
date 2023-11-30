using GlobalEnums;

public class Boss3Phase3 : BossSkill
{
    private EnemyDroneSpawner _enemyDroneSpawner;

    public Boss3Phase3(BossBehaviourTree bossBehaviourTree) : base(bossBehaviourTree)
    {
        _enemyDroneSpawner = new EnemyDroneSpawner(bossBehaviourTree.DroneSpawnTrans, bossBehaviourTree.DroneSpawnDuration);
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

        btDict[BTValues.CurrentAction] = CurrentAction.Attack;
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