using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    [SerializeField] float size;
    [SerializeField] Vector3 position;

    private List<EnemyMovementDataHandler> enemys = new List<EnemyMovementDataHandler>();
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
            enemys.Add(other.GetComponent<EnemyMovementDataHandler>());

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
            foreach(EnemyMovementDataHandler enemy in enemys)
            {
                // enemy.SetIsTracing(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemys.Remove(other.GetComponent<EnemyMovementDataHandler>());
        }

        if (other.gameObject.tag == "Player")
        {
            PlayerInArea = false;
            foreach (EnemyMovementDataHandler enemy in enemys)
            {
                // enemy.SetIsTracing(false);
            }
        }
    }

    private void SendAreaInfo(GameObject enemy)
    {
        
    }
}
