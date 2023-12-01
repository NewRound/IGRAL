using System;
using System.Net.Sockets;
using UnityEngine;

public class RPG : MonoBehaviour
{
    [SerializeField] private GameObject modelRocket;
    [SerializeField] private Rocket rocketPrefab;
    [SerializeField] private ParticleSystem shootParticle;
    private Transform _bulletSpawnTrans;

    private Rocket _rocket;

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
        if (_rocket == null)
        {
            _rocket = Instantiate(rocketPrefab, _bulletSpawnTrans.position, Quaternion.identity);
            _rocket.SetSpawnTrans(_bulletSpawnTrans);
        }
        else
            _rocket.gameObject.SetActive(true);
    }
}