using System;
using UnityEngine;

public class SkillUse : MonoBehaviour
{
    [SerializeField] private GameObject _skillIcon;
    [field: SerializeField] public SkillCategoryType skillCategoryType {  get; private set; }

    protected PlayerAppearanceController mutantController;
    protected PlayerSO curData;

    public float usingKcal;

    public float durationTime;
    public float _currentTime;
    
    protected bool _isLearned = false;
    protected bool _isActive = false;

    public Action<bool> SkillAction;

    private void Start()
    {
        mutantController = GameManager.Instance.PlayerTransform.GetComponent<PlayerAppearanceController>();
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

    public void UseSkill()
    {
        if (_isActive)
        {
            StopSkill();
            return;
        }

        if (!_isLearned || curData.Kcal < usingKcal)
            return;

        UIController.Instance.isSkill = false;
        SkillManager.Instance.AllOffSkill();
        SkillAction?.Invoke(true);
        _isActive = true;
    }
    
    public void StopSkill()
    {
        if (mutantController.mutantType != MutantType.None)
            mutantController.ChangeMutant(MutantType.None);
        _currentTime = 0;
        _isActive = false;
        SkillAction?.Invoke(false);
    }

    public virtual void UsingKcal(float kcal)
    {
        GameManager.Instance.StatHandler.BurnKcal(kcal);
    }
}
