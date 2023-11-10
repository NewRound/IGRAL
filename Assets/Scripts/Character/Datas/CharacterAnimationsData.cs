using System;
using UnityEngine;

[Serializable]
public class CharacterAnimationsData
{
    [SerializeField] private string moveParameter = "@Move";
    [SerializeField] private string attackParameter = "@Attack";
    [SerializeField] private string speedRatioParameter = "SpeedRatio";
    [SerializeField] private string dieParameter = "Die";

    public int MoveSubStateParameterHash { get; private set; }
    public int AttackSubStateParameterHash { get; private set; }
    public int SpeedRatioParameterHash { get; private set; }
    public int dieParameterHash { get; private set; }

    public virtual void Init()
    {
        MoveSubStateParameterHash = Animator.StringToHash(moveParameter);
        AttackSubStateParameterHash = Animator.StringToHash(attackParameter);
        SpeedRatioParameterHash = Animator.StringToHash(speedRatioParameter);
        dieParameterHash = Animator.StringToHash(dieParameter);
    }
}

