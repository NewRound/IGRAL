using UnityEngine;

public class AnimationUtil
{
    public static void ReStartIfAnimationIsPlaying(Animator animator, int animationParameterHash, int layerIndex = 0)
    {
        if (animator.GetCurrentAnimatorStateInfo(layerIndex).shortNameHash.Equals(animationParameterHash))
        {
            animator.Play(animationParameterHash, layerIndex, 0f);
        }
    }

    public static bool CheckCurrentAnimationEnded(Animator animator, int animationHash, float animationNormalizeEndedTime, int layerIndex = 0)
    {
        var stateInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);
        if (stateInfo.shortNameHash.Equals(animationHash))
        {
            if (stateInfo.normalizedTime >= animationNormalizeEndedTime)
                return true;
        }

        return false;
    }

    public static float GetNormalizeTime(Animator animator, int animationHash, int layerIndex = 0)
    {
        var stateInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);
        return stateInfo.normalizedTime;
    }



    public static float GetNormalizeTime(Animator animator, AnimTag animTag, int layerIndex = 0)
    {
        string tag = animTag.ToString();
        var currentStateInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);
        var nextStateInfo = animator.GetNextAnimatorStateInfo(layerIndex);

        if (animator.IsInTransition(layerIndex))
        {
            if (nextStateInfo.IsTag(tag))
                return nextStateInfo.normalizedTime;
        }
        else
        {
            if (currentStateInfo.IsTag(tag))
                return currentStateInfo.normalizedTime;
        }

        return 0;
    }
}