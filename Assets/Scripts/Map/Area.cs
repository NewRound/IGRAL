using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    [SerializeField] float size;
    [SerializeField] Vector3 position;

    [SerializeField] public List<EnemyController> enemys = new List<EnemyController>();
    private bool PlayerInArea = false;

    private void Awake()
    {
        position = transform.position;
        size = (MapGenerator.Instance.tileSize * size) - 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            enemys.Add(other.GetComponent<EnemyController>());

            // enemy ==> float, float <발판의 중앙 x 값,  길이의 -1 값.>
            SendAreaInfo(other.gameObject);

            if(PlayerInArea)
            {
                // enemy.SetIsTracing(true);
            }
        }

        if (other.gameObject.tag == "Player")
        {
            PlayerInArea = true;
            foreach(EnemyController enemy in enemys)
            {
                // enemy.SetIsTracing(true);
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
                // enemy.SetIsTracing(false);
            }
        }
    }

    private void SendAreaInfo(GameObject enemy)
    {
        Debug.Log(position.x);
        Debug.Log(transform.position.x);
        enemy.GetComponent<EnemyController>().StateMachine.SetAreaData(position.x, size);
    }
}
