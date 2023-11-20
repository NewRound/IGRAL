using UnityEngine;

public class SkillKnife : SkillUse
{
    public override void UpdataSkillData()
    { 
        _currentTime = 0f;
        usingKcal = SkillManager.Instance.knifeData.UsingKcal;
        durationKcal = SkillManager.Instance.knifeData.DurationKcal;
        durationTime = SkillManager.Instance.knifeData.DurationTime;
    }

    private void Update()
    {
        if (_isActive)
        {
            if(mutantController.mutantType != MutantType.Blade)
                mutantController.ChangeMutant(MutantType.Blade);

            UsingKcal(durationKcal * Time.deltaTime);

            if (curData.Kcal <= 0) 
            {
                StopSkill();
            }
        }
    }
}
