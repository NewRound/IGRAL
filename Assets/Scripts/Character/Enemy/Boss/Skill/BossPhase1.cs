using GlobalEnums;
using UnityEngine;

public class BossPhase1 : BossSkill
{
    private BossGranade _granade;

    public BossPhase1(BossBehaviorTree bossBehaviourTree) : base(bossBehaviourTree)
    {
        Init();
    }

    public override NodeState Evaluate()
    {
        if (!IsActionPossible((CurrentAction)btDict[BTValues.CurrentAction], CurrentAction.UsingSkill) 
            || bossBehaviourTree.CurrentPhase != 1)
        {
            state = NodeState.Failure;
            return state;
        }

        if ((bool)btDict[BTValues.IsAttacking])
        {
            float normalizedTime = AnimationUtil.GetNormalizeTime(animationController.Animator, AnimTag.Skill, (int)AnimatorLayer.UpperLayer);
            if (normalizedTime > 1f)
            {
                _granade.transform.position = defaultWeapon.transform.position;
                _granade.ThrowGranade();
                OnAnimationEnded();

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

    protected override void Init()
    {
        _granade = Object.Instantiate(
            Resources.Load<BossGranade>("Boss/SkillWeapons/Granade"), 
            defaultWeapon.transform.position, Quaternion.identity);
        _granade.DeActivate();
    }

    protected override void OnChargedCoolTime()
    {
        base.OnChargedCoolTime();
        _granade.gameObject.SetActive(true);
        defaultWeapon.SetActive(false);
        UseSkill();
    }
}