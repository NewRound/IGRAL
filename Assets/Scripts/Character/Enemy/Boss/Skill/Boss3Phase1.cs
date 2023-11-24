using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Phase1 : Boss3PhaseBase
{
    private GameObject _defaultWeapon;
    private RPG _newWeapon;
    private Transform _bulletSpawnTrans;
    private BossAnimationController _animationController;

    public Boss3Phase1(GameObject defaultWeapon, Transform bulletSpawnTrans, BossAnimationController animationController)
    {
        _defaultWeapon = defaultWeapon;
        _bulletSpawnTrans = bulletSpawnTrans;
        _animationController = animationController;
        Init();
    }

    public override void UseSkill()
    {
        _newWeapon.Shoot();
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

    
}
