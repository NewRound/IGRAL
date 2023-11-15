using UnityEngine;

public class EnemyAnimationController : AnimationController
{
    [field: SerializeField] public EnemyAnimationsData AnimationData { get; private set; }

    public override void Init()
    {
        base.Init();
        AnimationData.Init();
    }
}
