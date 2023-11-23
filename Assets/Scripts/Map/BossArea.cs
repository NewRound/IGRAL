using System.Collections.Generic;
using UnityEngine;

public class BossArea : MonoBehaviour
{
    [Header("AreaData")]
    [SerializeField] public float size;
    [SerializeField] public Vector3 position;

    [SerializeField] public List<EnemyController> enemys = new List<EnemyController>();
    private bool PlayerInArea = false;

    private void Start()
    {
        position = transform.position;
        size = (MapGenerator.Instance.tileSize * size) - 1;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {

            enemys.Add(other.GetComponent<EnemyController>());
            other.GetComponent<EnemyController>().StatHandler.DieAction += UpdateEnemyDied;
            // enemy ==> float, float <발판의 중앙 x 값,  길이의 -1 값.>
            SendAreaInfo(other.gameObject);

            if (PlayerInArea)
            {
                enemys[enemys.Count - 1].StateMachine.SetIsTracing(true);
            }
        }

        if (other.gameObject.tag == "Player")
        {
            PlayerInArea = true;
            foreach (EnemyController enemy in enemys)
            {
                enemy.StateMachine.SetIsTracing(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemys.Remove(other.GetComponent<EnemyController>());
        }

        if (other.gameObject.tag == "Player")
        {
            PlayerInArea = false;
            foreach (EnemyController enemy in enemys)
            {
                enemy.StateMachine.SetIsTracing(false);
            }
        }
    }

    private void SendAreaInfo(GameObject enemy)
    {
        EnemyStateMachine enemyStateMachine = enemy.GetComponent<EnemyController>().StateMachine;
        enemyStateMachine.SetAreaData(transform.position.x, size);
        enemyStateMachine.Init();
    }

    void UpdateEnemyDied()
    {
        foreach (EnemyController enemy in enemys)
        {
            if (enemy.StateMachine.IsDead)
            {
                enemys.Remove(enemy);
                return;
            }
        }
    }
}
