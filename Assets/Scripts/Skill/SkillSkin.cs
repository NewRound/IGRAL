using UnityEngine;

public class SkillSkin : SkillUse
{
    public float durationTime = 5f;
    private float CurrentTime = 0f;

    public override void UseSkill()
    {
        if (!_isLearned || curData.Kcal < usingKcal)
            return;

        UIController.Instance.isSkill = false;
        Debug.Log("스킨 사용");
        SkillManager.Instance.AllOffSkill();
        _isActive = true;
    }

    private void Awake()
    {
        // 스킬 언락에 따른 지속시간 증가시 작성할 것.
        durationTime = 5f;

        usingKcal = -100.0f;
    }

    private void Update()
    {
        if (_isActive)
        {
            if (mutantController.mutantType != MutantType.Skin)
            { 
                mutantController.ChangeMutant(MutantType.Skin);
                UsingKcal(usingKcal);
                Debug.Log($"{curData.Kcal}");
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
