using GlobalEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class ExplosionWeapon : Weapon
{
    [SerializeField] protected ParticleSystem[] explosionParticles;
    [SerializeField] protected MeshRenderer meshRenderer;
    [SerializeField] protected float damage = 15f;
    [SerializeField] protected float explosionDelayTime = 2f;
    [SerializeField] protected float explosionTime = 2f;
    [SerializeField] protected float explosionRange = 2f;

    [SerializeField] protected float rotationSpeed = 90f;

    [SerializeField] protected float yOffset = 0.5f;

    protected float currentElapsedTime;
    protected float halfExplosiondelayTime;

    protected Dictionary<float, WaitForSeconds> explosionDict = new Dictionary<float, WaitForSeconds>();

    protected Material material;
    protected GameObject modelObject;

    private LayerMask _playerLayer;

    protected void OnDisable()
    {
        ResetValues();
    }

    public virtual void Activate()
    {
        ActivateModel();
    }

    protected void ActivateModel()
    {
        modelObject.SetActive(true);
    }

    public virtual void DeActivate()
    {
        DeActivateModel();
    }

    protected void DeActivateModel()
    {
        modelObject.SetActive(false);
    }

    protected virtual void Init()
    {
        material = meshRenderer.material;
        modelObject = meshRenderer.gameObject;
        _playerLayer = 1 << LayerMask.NameToLayer(Tag.Player.ToString());
        halfExplosiondelayTime = GlobalValues.HALF * explosionDelayTime;
        explosionDict.Add(halfExplosiondelayTime, CoroutineRef.GetWaitForSeconds(halfExplosiondelayTime));
        explosionDict.Add(explosionTime, CoroutineRef.GetWaitForSeconds(explosionTime));
    }

    protected virtual IEnumerator Explode()
    {
        yield return StartCoroutine(ChangeColor());
        ApplyExplosionDamage();
        StartCoroutine(DestroySelf());
    }

    protected IEnumerator ChangeColor()
    {
        material.DOColor(Color.yellow, halfExplosiondelayTime);
        yield return explosionDict[halfExplosiondelayTime];

        material.DOColor(Color.red, halfExplosiondelayTime);
        yield return explosionDict[halfExplosiondelayTime];
    }

    protected void ApplyExplosionDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRange, _playerLayer);
        if (colliders.Length > 0)
        {
            PlayerStatHandler statHandler = colliders[0].GetComponent<PlayerController>().StatHandler;
            Attack(damage, statHandler.Data, statHandler);
        }
    }


    protected virtual void ResetValues()
    {
        if (material != null)
            material.color = Color.white;
    }

    protected virtual IEnumerator DestroySelf()
    {
        DeActivate();

        foreach (ParticleSystem particleSystem in explosionParticles)
            particleSystem.Play();

        yield return explosionDict[explosionTime];

        gameObject.SetActive(false);
    }
}
