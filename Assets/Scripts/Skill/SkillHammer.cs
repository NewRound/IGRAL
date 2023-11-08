using UnityEngine;

public class SkillHammer : SkillUse
{
    private void Awake()
    {
        usingKcal = 2.0f;
    }

    public override void UseSkill()
    {
        if (!_isLearned || curData.Kcal < usingKcal)
            return;

        UIController.Instance.isSkill = false;
        SkillManager.Instance.AllOffSkill();
        _isActive = true;
    }

    private void Update()
    {
        if(_isActive)
        {
            if (mutantController.mutantType != MutantType.Stone)
                mutantController.ChangeMutant(MutantType.Stone);


            UsingKcal(usingKcal * Time.deltaTime);

            if (curData.Kcal <= 0)
            {
                StopSkill();
            }
        }
    }
}
