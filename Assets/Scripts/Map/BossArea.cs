using System.Collections.Generic;
using UnityEngine;

public class BossArea : MonoBehaviour
{
    [Header("AreaData")]
    [SerializeField] private Transform SpawnPoint;
    [SerializeField] private Transform[] Waypoints;
    // [SerializeField] private GameObject[] Platform;

    [SerializeField] public GameObject escapeButtonObject;

    [SerializeField] private GameObject BossPrefab;
    private GameObject _boss;

    private bool BossSpawned = false;
    private bool BossDied = false;


    private void Update()
    {
        if(_boss == null && !BossDied)
        {
            BossIsDead();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        InputController player = other.GetComponent<InputController>();
        if(player != null)
        {
            if(!BossSpawned)
                SpawnBoss();
        }
    }


    public void BossIsDead()
    {
        if (!BossDied)
        {
            escapeButtonObject.SetActive(true);
            BossDied = true;
        }
    }

    public void SpawnBoss()
    {
        GameObject Boss = Instantiate(BossPrefab);
        Boss.transform.position = SpawnPoint.position;
        Boss.GetComponent<BossBehaviourTree>().Init(Waypoints);
        _boss = Boss;

        BossSpawned = true;
    }
}
