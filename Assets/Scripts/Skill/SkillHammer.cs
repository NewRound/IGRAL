using UnityEngine;

public class SkillHammer : SkillUse
{
    public override void UpdataSkillData()
    {
        _currentTime = 0f;
        usingKcal = SkillManager.Instance.hammerData.UsingKcal;
        durationKcal = SkillManager.Instance.hammerData.DurationKcal;
        durationTime = SkillManager.Instance.hammerData.DurationTime;
    }

    private void Update()
    {
        if(_isActive)
        {
            if (mutantController.mutantType != MutantType.Stone)
                mutantController.ChangeMutant(MutantType.Stone);

            UsingKcal(durationKcal * Time.deltaTime);

            if (curData.Kcal <= 0)
            {
                StopSkill();
            }
        }
    }
}
