using UnityEngine;

public class SkillPsychometric : SkillUse
{
    public override void UpdataSkillData()
    {
        _currentTime = 0f;
        usingKcal = SkillManager.Instance.psychometricrData.UsingKcal;
        durationKcal = SkillManager.Instance.psychometricrData.DurationKcal;
        durationTime = SkillManager.Instance.psychometricrData.DurationTime;
    }

    private void Update()
    {
        if (_isActive)
        {
            if (mutantController.mutantType != MutantType.Sheld)
            {
                mutantController.ChangeMutant(MutantType.Sheld);
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
