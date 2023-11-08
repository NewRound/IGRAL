using UnityEngine;

public class SkillPsychometric : SkillUse
{
    public float durationTime = 5.0f;
    private float CurrentTime = 0f;

    public override void UseSkill()
    {
        if (!_isLearned || curData.Kcal < usingKcal)
            return;

        UIController.Instance.isSkill = false;
        SkillManager.Instance.AllOffSkill();
        _isActive = true;
    }

    private void Awake()
    {
        durationTime = 5f;
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
            CurrentTime += Time.deltaTime;

            if (CurrentTime >= durationTime)
            {
                CurrentTime = 0f;
                StopSkill();
            }
        }
    }
}
