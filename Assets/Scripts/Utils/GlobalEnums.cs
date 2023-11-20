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
    }

    public enum NodeState
    {
        Running,
        Success,
        Failure,
    }
}
