using System.Collections;
using UnityEngine;

public class SkillUse : MonoBehaviour
{
    [SerializeField] private GameObject _skillIcon;
    [field: SerializeField] public SkillCategoryType skillCategoryType {  get; private set; }
    protected PlayerController playerController;
    protected PlayerAppearanceController mutantController;
    protected bool _isLearned = false;
    protected bool _isActive = false;

    private void Start()
    {
        playerController = GameManager.Instance.player.GetComponent<PlayerController>();
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
        
        if (mutantController.mutantType != MutantType.None)
            mutantController.ChangeMutant(MutantType.None);
        
        _isActive = false;
    }
}
