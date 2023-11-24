using System;
using UnityEngine;

public class RPG : MonoBehaviour
{
    [SerializeField] private GameObject modelRocket;
    [SerializeField] private Rocket rocketPrefab;
    [SerializeField] private ParticleSystem shootParticle;
    private Transform _bulletSpawnTrans;

    public void Init(Transform bulletSpawnTrans)
    {
        _bulletSpawnTrans = bulletSpawnTrans;
    }

    private void OnEnable()
    {
        ActivateRocket();
    }
    
    private void ActivateRocket()
    {
        modelRocket.SetActive(true);
    }

    public void DeActivateRocket()
    {
        modelRocket.SetActive(false);
    }

    public void Shoot()
    {
        shootParticle.Play();
        Instantiate(rocketPrefab, _bulletSpawnTrans.position, Quaternion.identity);
    }
}