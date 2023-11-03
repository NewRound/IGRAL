using UnityEngine;

public class SkillUse : MonoBehaviour
{
    [SerializeField] private GameObject _skillIcon;
    [field: SerializeField] public SkillCategoryType skillCategoryType {  get; private set; }
    private bool _isLearned = false;

    public void LearnedSkill()
    {
        _isLearned = true;
    }

    public void DisplaySkill()
    {
        _skillIcon.SetActive(_isLearned);
    }

    public void NoDisplaySkill()
    {
        if(_isLearned)
        {
            _skillIcon.SetActive(false);
        }
    }

    public virtual void UseSkill()
    {
        if (!_isLearned)
            return;
        UIController.Instance.isSkill = false;
    }
}
