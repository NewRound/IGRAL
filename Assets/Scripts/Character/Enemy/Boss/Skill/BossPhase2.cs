using GlobalEnums;
using UnityEngine;

public class BossPhase2 : BossSkill
{
    private RPG _newWeapon;
    private Transform _bulletSpawnTrans;
    public BossPhase2(BossBehaviourTree bossBehaviourTree) : base(bossBehaviourTree)
    {
        _bulletSpawnTrans = bossBehaviourTree.BulletSpawnTrans;
        btDict = bossBehaviourTree.BTDict;
        SetAttackEvent();
        Init();
    }

    public override NodeState Evaluate()
    {
        if (!IsActionPossible((CurrentAction)btDict[BTValues.CurrentAction], CurrentAction.UsingSkill)
            || bossBehaviourTree.CurrentPhase != 2)
        {
            animationController.AttackAction -= _newWeapon.Shoot;
            state = NodeState.Failure;
            return state;
        }

        if ((bool)btDict[BTValues.IsAttacking])
        {
            float normalizedTime = AnimationUtil.GetNormalizeTime(animationController.Animator, AnimTag.Skill, (int)AnimatorLayer.UpperLayer);
            if (normalizedTime > 1f)
            {
                animationController.AttackAction -= _newWeapon.Shoot;
               
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

    protected override void OnChargedCoolTime()
    {
        base.OnChargedCoolTime();
        animationController.AttackAction += _newWeapon.Shoot;
        UseSkill();
    }

    public void ChangeToNewWeapon()
    {
        defaultWeapon.SetActive(false);
        _newWeapon.gameObject.SetActive(true);
    }

    public void ChangeToDefaultWeapon()
    {
        defaultWeapon.SetActive(true);
        _newWeapon.gameObject.SetActive(false);
    }

    protected override void Init()
    {
        if (_newWeapon == null)
            _newWeapon = UnityEngine.Object.Instantiate(
                Resources.Load<RPG>("Boss/EquippedWeapons/RPG"), 
                defaultWeapon.transform.parent);
        
        _newWeapon.gameObject.SetActive(false);
        _newWeapon.Init(_bulletSpawnTrans);
    }

    private void SetAttackEvent()
    {
        animationController.PreSkillAction += ChangeToNewWeapon;
        animationController.PostSkillAction += ChangeToDefaultWeapon;
    }


}
