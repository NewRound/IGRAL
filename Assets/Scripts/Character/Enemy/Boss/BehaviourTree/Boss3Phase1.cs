using GlobalEnums;
using UnityEngine;

public class Boss3Phase1 : BossSkill
{
    private BossGranade _granade;

    public Boss3Phase1(BossBehaviourTree bossBehaviourTree) : base(bossBehaviourTree)
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

        btDict[BTValues.CurrentAction] = CurrentAction.Attack;
        state = NodeState.Failure;
        return state;
    }

    protected override void Init()
    {
        _granade = Object.Instantiate(
            Resources.Load<BossGranade>("Boss/SkillWeapons/Granade"), 
            defaultWeapon.transform.position, Quaternion.identity);
        _granade.DeActivateModel();
    }

    protected override void OnChargedCoolTime()
    {
        base.OnChargedCoolTime();
        defaultWeapon.SetActive(false);
        _granade.transform.position = defaultWeapon.transform.position;
        _granade.ActivateModel();
        _granade.InitPos();
        UseSkill();
    }
}