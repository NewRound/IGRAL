using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDroneBullet : ExplosionWeapon
{
    private EffectManager _effectManager;

    protected override void ResetValues()
    {
        base.ResetValues();
    }

    private void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        _effectManager = EffectManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();

        if (playerController != null)
        {
            Attack(damage, playerController.StatHandler.Data, playerController.StatHandler);
        }

        StartCoroutine(DestroySelf());
    }

    protected override IEnumerator DestroySelf()
    {
        DeActivate();

        GameObject explosion = _effectManager.GetEffect(EffectType.Bomb);
        explosion.transform.position = transform.position;
        yield return explosionDict[explosionTime];

        explosion.SetActive(false);
        gameObject.SetActive(false);
    }

    public void SetDirection(Vector3 direction)
    {
        modelObject.transform.up = -direction;
    }
}
