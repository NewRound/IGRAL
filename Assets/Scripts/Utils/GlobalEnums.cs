using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalEnums
{
    public enum AnimatorLayer
    {
        BaseLayer,
        UpperLayer,
    }

    public enum Tag
    {
        Player,
        Enemy,
        Interactable
    }

    public enum NodeState
    {
        Running,
        Success,
        Failure,
    }

    public enum BTValues
    {
        CurrentPhaseSkillCoolTime,
        CurrentSkillElapsedTime,
        WasSkillUsed,
    }
}
