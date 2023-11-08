using UnityEngine;

public class SkillKnife : SkillUse
{
    private void Awake()
    {
        usingKcal = 1.0f;
    }

    private void Update()
    {
        if (_isActive)
        {
            if(mutantController.mutantType != MutantType.Blade)
                mutantController.ChangeMutant(MutantType.Blade);

            UsingKcal(usingKcal * Time.deltaTime);

            if (curData.Kcal <= 0) 
            {
                StopSkill();
            }
        }
    }
}
