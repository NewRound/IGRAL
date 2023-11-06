using System.Collections;
using UnityEngine;

public class SkillUse : MonoBehaviour
{
    [SerializeField] private GameObject _skillIcon;
    [field: SerializeField] public SkillCategoryType skillCategoryType {  get; private set; }

    protected PlayerAppearanceController mutantController;
    protected PlayerSO curData;

    public float usingKcal = 0;

    protected bool _isLearned = false;
    protected bool _isActive = false;

    private void Start()
    {
        mutantController = GameManager.Instance.player.GetComponent<PlayerAppearanceController>();
        curData = GameManager.Instance.StatHandler.Data;
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

    public virtual void UsingKcal(float kcal)
    {
        GameManager.Instance.StatHandler.BurnKcal(kcal);
    }
}
