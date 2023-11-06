using UnityEngine;

public class SkillPsychometric : SkillUse
{
    public float durationTime = 5.0f;
    private float CurrentTime = 0f;

    public override void UseSkill()
    {
        if (!_isLearned)
            return;

        UIController.Instance.isSkill = false;
        Debug.Log("사이코 메트릭 사용");
        _isActive = true;
    }

    private void Awake()
    {
        durationTime = 5f;
        Debug.Log($"{durationTime}");
    }

    private void Update()
    {
        if (_isActive)
        {
            if (mutantController.mutantType != MutantType.Sheld)
                mutantController.ChangeMutant(MutantType.Sheld);
            
            CurrentTime += Time.deltaTime;

            if (CurrentTime >= durationTime)
            {
                CurrentTime = 0f;
                _isActive = false;
                mutantController.ChangeMutant(MutantType.None);
            }

            // if(kcal <= 0) {break};
            // kcal reduce
        }
    }
}
