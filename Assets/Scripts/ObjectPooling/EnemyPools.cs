using UnityEngine;

public class EnemyPools : MonoBehaviour
{
    [field: SerializeField] public EnemyPool[] EnemyPoolPrefabs { get; private set; }
}
