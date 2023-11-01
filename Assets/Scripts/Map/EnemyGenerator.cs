using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private List<Vector3> SpawnPoints;
    [SerializeField] private List<GameObject> Enemys;

    private void Awake()
    {
        
    }

    private void Start()
    {
        foreach(Vector3 position in SpawnPoints)
        {
            int randomValue = Random.Range(0, Enemys.Count);
            GameObject enemy = Instantiate(Enemys[randomValue]);

            enemy.transform.position = position;
        }
    }
}
