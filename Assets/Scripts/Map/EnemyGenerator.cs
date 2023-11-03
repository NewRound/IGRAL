using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> Enemys;

    public GameObject InstantiateEnemy()
    {
        int randomValue = Random.Range(0, Enemys.Count);
        return Instantiate(Enemys[randomValue]);
    }
}