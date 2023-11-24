using GlobalEnums;
using System.Collections.Generic;

public class CheckAttackPossibleNode : Node
{
    private BossBehaviourTree _bossBehaviourTree;
    private BossAnimationController _animationController;
    private Dictionary<BTValues, object> _btDict = new Dictionary<BTValues, object>();

    public CheckAttackPossibleNode(BossBehaviourTree bossBehaviourTree)
    {
        _bossBehaviourTree = bossBehaviourTree;
        _animationController = _bossBehaviourTree.AnimationController;
        _btDict = _bossBehaviourTree.BTDict;
    }

    public override NodeState Evaluate()
    {
        float normalizedTime = 0f;
        if ((bool)_btDict[BTValues.IsAnyActionPlaying])
        {
            if ((bool)_btDict[BTValues.WasSkillUsed])
            {
                normalizedTime = AnimationUtil.GetNormalizeTime(_animationController.Animator, AnimTag.Skill, (int)AnimatorLayer.UpperLayer);
                if (normalizedTime > 1f)
                {
                    _animationController.PlayAnimation(_animationController.AnimationData.AttackSubStateParameterHash, false);
                    _animationController.PlayAnimation(_animationController.AnimationData.SkillSubParameterHash, false);
                    _btDict[BTValues.IsAnyActionPlaying] = false;
                    _btDict[BTValues.WasSkillUsed] = false;
                    _animationController.ResetAttackEvent();
                    state = NodeState.Running;
                    return state;
                }
            }
            else
            {
                normalizedTime = AnimationUtil.GetNormalizeTime(_animationController.Animator, AnimTag.Attack, (int)AnimatorLayer.UpperLayer);
                if (normalizedTime > 1f)
                {
                    _btDict[BTValues.IsAnyActionPlaying] = false;
                    _animationController.PlayAnimation(_animationController.AnimationData.AttackSubStateParameterHash, false);
                    state = NodeState.Running;
                    return state;
                }
            }
        }
        else
        {
            state = NodeState.Success;
            return state;
        }

        state = NodeState.Running; 
        return state;
    }
}