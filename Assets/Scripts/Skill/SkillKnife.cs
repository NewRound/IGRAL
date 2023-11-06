using UnityEngine;

public class SkillKnife : SkillUse
{
    private void Awake()
    {
        usingKcal = 1.0f;
    }

    public override void UseSkill()
    {
        if (!_isLearned || curData.Kcal < usingKcal)
            return;

        UIController.Instance.isSkill = false;
        Debug.Log("나이프 사용");
        SkillManager.Instance.AllOffSkill();
        _isActive = true;
    }

    private void Update()
    {
        if (_isActive)
        {
            if(mutantController.mutantType != MutantType.Blade)
                mutantController.ChangeMutant(MutantType.Blade);

            UsingKcal(usingKcal * Time.deltaTime);
            Debug.Log($"{curData.Kcal}");

            if (curData.Kcal <= 0) 
            {
                StopSkill();
            }
            
            Debug.Log($"kcal : {-1 * Time.deltaTime}");
        }
    }
}
