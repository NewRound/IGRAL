using GlobalEnums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Phase2 : BossSkill
{
    private GameObject _defaultWeapon;
    private RPG _newWeapon;
    private Transform _bulletSpawnTrans;
    public Boss3Phase2(BossBehaviourTree bossBehaviourTree) : base(bossBehaviourTree)
    {
        _defaultWeapon = bossBehaviourTree.DefaultWeapon;
        _bulletSpawnTrans = bossBehaviourTree.BulletSpawnTrans;
        btDict = bossBehaviourTree.BTDict;
        Init();
    }

    public override NodeState Evaluate()
    {
        if (!IsActionPossible((CurrentAction)btDict[BTValues.CurrentAction], CurrentAction.UsingSkill))
        {
            state = NodeState.Failure;
            return state;
        }

        if ((float)btDict[BTValues.CurrentSkillElapsedTime] >= (float)btDict[BTValues.CurrentPhaseSkillCoolTime])
        {
            return OnChargedCoolTime();
        }

        float normalizedTime = AnimationUtil.GetNormalizeTime(animationController.Animator, AnimTag.Skill, (int)AnimatorLayer.UpperLayer);
        if (normalizedTime > 1f)
        {
            animationController.PlayAnimation(animationController.AnimationData.AttackSubStateParameterHash, false);
            animationController.PlayAnimation(animationController.AnimationData.SkillSubParameterHash, false);

            btDict[BTValues.CurrentAction] = CurrentAction.Patrol;

            state = NodeState.Success;
            return state;
        }

        state = NodeState.Running;
        return state;
    }

    private NodeState OnChargedCoolTime()
    {
        btDict[BTValues.CurrentSkillElapsedTime] = 0f;

        animationController.AttackAction += _newWeapon.Shoot;
        UseSkill();
        state = NodeState.Success;
        return state;
    }

    public void ChangeToNewWeapon()
    {
        _defaultWeapon.SetActive(false);
        _newWeapon.gameObject.SetActive(true);
    }

    public void ChangeToDefaultWeapon()
    {
        _defaultWeapon.SetActive(true);
        _newWeapon.gameObject.SetActive(false);
    }

    private void Init()
    {
        if (_newWeapon == null)
            _newWeapon = UnityEngine.Object.Instantiate(Resources.Load<RPG>("Boss/EquippedWeapons/RPG"), _defaultWeapon.transform.parent);
        
        _newWeapon.gameObject.SetActive(false);
        _newWeapon.Init(_bulletSpawnTrans);
    }

    private void SetAttackEvent()
    {
        animationController.PreSkillAction += ChangeToNewWeapon;
        animationController.PostSkillAction += ChangeToDefaultWeapon;
    }
}
