using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AnimationController : MonoBehaviour
{
    [field: SerializeField] public CharacterAnimationsData AnimationData { get; private set; }
    private Animator _animator;

    [field: SerializeField] public float animationNormalizeEndedTime = 0.9f;

    public void Init()
    {
        _animator = GetComponent<Animator>();
        AnimationData.Init();
    }

    public void PlayAnimation(int animationParameterHash, bool isPlaying)
    {
        _animator.SetBool(animationParameterHash, isPlaying);
    }

    public void PlayAnimation(int animationParameterHash)
    {
        _animator.SetTrigger(animationParameterHash);
    }

    public void PlayAnimation(int animationParameterHash, int integerValue)
    {
        _animator.SetInteger(animationParameterHash, integerValue);
    }

    public void PlayAnimation(int animationParameterHash, float floatValue)
    {
        _animator.SetFloat(animationParameterHash, floatValue);
    }


    public void ReStartIfAnimationIsPlaying(int animationParameterHash, int layerIndex = 0)
    {
        if (_animator.GetCurrentAnimatorStateInfo(layerIndex).shortNameHash.Equals(animationParameterHash))
        {
            _animator.Play(animationParameterHash, layerIndex, 0f);
        }
    }

    public bool CheckCurrentAnimationEnded(int animationParameterHash, int layerIndex = 0)
    {
        var stateInfo = _animator.GetCurrentAnimatorStateInfo(layerIndex);
        if (stateInfo.shortNameHash.Equals(animationParameterHash))
        {
            if (stateInfo.normalizedTime >= animationNormalizeEndedTime)
            {
                return true;
            }
        }

        return false;
    }

    public bool CheckNextAnimationEnded(int animationParameterHash, int layerIndex = 0)
    {
        var stateInfo = _animator.GetCurrentAnimatorStateInfo(layerIndex);
        Debug.Log($"stateInfo : {stateInfo.shortNameHash}\n SpeedRatioParameterHash : {AnimationData.SpeedRatioParameterHash}\n MoveParameterHash : {AnimationData.MoveParameterHash}");
        if (stateInfo.shortNameHash.Equals(animationParameterHash))
        {
            if (stateInfo.normalizedTime >= animationNormalizeEndedTime)
            {
                return true;
            }
        }

        return false;
    }
}
