using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDroneBullet : ExplosionWeapon
{
    private bool _isTimeOver;

    protected override void ResetValues()
    {
        base.ResetValues();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();

        if (playerController != null)
        {
            if (_isTimeOver)
                return;
            Attack(damage, playerController.StatHandler.Data, playerController.StatHandler);
            StartCoroutine(DestroySelf());
        }
    }
}
