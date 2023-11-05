using System.Collections;
using UnityEngine;

public class SkillUse : MonoBehaviour
{
    [SerializeField] private GameObject _skillIcon;
    [field: SerializeField] public SkillCategoryType skillCategoryType {  get; private set; }
    protected PlayerAppearanceController mutantController;
    protected bool _isLearned = false;
    protected bool _isActive = false;

    private void Start()
    {
        mutantController = GameManager.Instance.player.GetComponent<PlayerAppearanceController>();
    }

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

    }
    
    public void StopSkill()
    {
        _isActive = false;
    }
}
