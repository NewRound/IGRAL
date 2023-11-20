using UnityEngine;

public class SkillSkin : SkillUse
{

    private void Awake()
    {
        // 스킬 언락에 따른 지속시간 증가시 작성할 것.
        durationTime = 5f;
        _currentTime = 0f;
        usingKcal = 100.0f;
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
