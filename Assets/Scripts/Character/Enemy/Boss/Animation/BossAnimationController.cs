using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationController : AnimationController
{
    public BossAnimationData AnimationData { get; private set; }

    public override void Init()
    {
        base.Init();
        AnimationData.Init();
    }
}
