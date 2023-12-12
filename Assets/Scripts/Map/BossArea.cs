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
    private bool PlayerInBossRoom = false;


    private void Update()
    {
        if (_boss == null && !BossDied && PlayerInBossRoom)
        {
            BossIsDead();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        InputController player = other.GetComponent<InputController>();
        if(player != null)
        {
            if (!BossSpawned)
            {
                SpawnBoss();
                
                PlayerInBossRoom = true;
            }
        }
    }


    public void BossIsDead()
    {
        if (!BossDied)
        {
            AudioManager.Instance.SetStage(2);
            escapeButtonObject.SetActive(true);
            BossDied = true;
        }
    }

    public void SpawnBoss()
    {
        GameObject Boss = Instantiate(BossPrefab);
        Boss.transform.position = SpawnPoint.position;
        Boss.GetComponent<BossBehaviorTree>().Init(Waypoints);
        AudioManager.Instance.EnterBossRoom();
        _boss = Boss;

        BossSpawned = true;
    }
}
