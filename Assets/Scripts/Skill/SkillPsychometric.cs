using UnityEngine;

public class SkillPsychometric : SkillUse
{

    private void Awake()
    {
        durationTime = 5f;
        _currentTime = 0f;
        usingKcal = 50f;
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
