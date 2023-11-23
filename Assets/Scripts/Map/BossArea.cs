using System.Collections.Generic;
using UnityEngine;

public class BossArea : MonoBehaviour
{
    [Header("AreaData")]
    [SerializeField] public float size;
    [SerializeField] public Vector3 position;
    [SerializeField] private Transform SpawnPoint;
    [SerializeField] private Transform[] Waypoints;
    [SerializeField] private GameObject[] Platform;

    [SerializeField] public IObject escapeObject;

    [SerializeField] private GameObject BossPrefab;

    private bool PlayerInArea = false;
    private bool BossSpawned = false;
    private bool BossDied = false;


    private void Start()
    {
        position = transform.position;
        size = (MapGenerator.Instance.tileSize * size) - 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        InputController player = other.GetComponent<InputController>();
        if(player != null)
        {
            PlayerInArea = true;
            if(!BossSpawned)
                SpawnBoss();
        }
    }

    private void SendAreaInfo(GameObject enemy)
    {
        EnemyStateMachine enemyStateMachine = enemy.GetComponent<EnemyController>().StateMachine;
        enemyStateMachine.SetAreaData(transform.position.x, size);
        enemyStateMachine.Init();
    }

    public void BossIsDead()
    {
        if (!BossDied)
        {
            escapeObject.Use();
            BossDied = true;
        }
    }

    public void SpawnBoss()
    {
        GameObject Boss = Instantiate(BossPrefab);
        Boss.transform.position = SpawnPoint.position;
        
        BossSpawned = true;
    }
}
