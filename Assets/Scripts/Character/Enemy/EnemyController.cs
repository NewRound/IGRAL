using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemySO stat;

    public EnemyStatHandler StatHandler { get; private set; }

    private void Awake()
    {
        StatHandler = new EnemyStatHandler(stat);
    }
}
