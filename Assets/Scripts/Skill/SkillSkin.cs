using UnityEngine;

public class SkillSkin : SkillUse
{
    public override void UpdataSkillData()
    {
        _currentTime = 0f;
        usingKcal = SkillManager.Instance.skinData.UsingKcal;
        durationKcal = SkillManager.Instance.skinData.DurationKcal;
        durationTime = SkillManager.Instance.skinData.DurationTime;
    }

    private void Update()
    {
        if (_isActive)
        {
            if (mutantController.mutantType != MutantType.Skin)
            { 
                mutantController.ChangeMutant(MutantType.Skin);
                UsingKcal(usingKcal);
            }

            _currentTime += Time.deltaTime;

            if (_currentTime >= durationTime)
            {
                _currentTime = 0f;
                StopSkill();
            }
        }
    }
}
