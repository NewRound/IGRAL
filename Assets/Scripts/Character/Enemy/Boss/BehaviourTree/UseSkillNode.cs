using GlobalEnums;
using System.Collections.Generic;
using UnityEngine;

public class UseSkillNode : Node
{
    private BossBehaviourTree _bossBehaviourTree;
    private PhaseSO _phaseSO;
    private Dictionary<BTValues, object> _btDict = new Dictionary<BTValues, object>();

    public UseSkillNode(BossBehaviourTree bossBehaviourTree)
    {
        _bossBehaviourTree = bossBehaviourTree;
        _phaseSO = _bossBehaviourTree.PhaseSO;
        _btDict = _bossBehaviourTree.BTDict;
    }

    public override NodeState Evaluate()
    {
        _btDict[BTValues.WasSkillUsed] = true;
        //_phaseSO.PhaseInfo[_bossBehaviourTree.CurrentPhase - 1].weaponPrefab;
        state = NodeState.Success; 
        return state;
    }
}