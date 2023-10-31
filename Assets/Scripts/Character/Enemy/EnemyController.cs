using UnityEngine;

public class EnemyController : CharacterController
{
    [SerializeField] private EnemySO stat;

    public EnemyStatHandler StatHandler { get; private set; }

    protected override void Awake()
    {
        StatHandler = new EnemyStatHandler(stat);
    }
}
