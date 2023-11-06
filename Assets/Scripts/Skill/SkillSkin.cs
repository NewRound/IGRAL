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
        Debug.Log("��Ų ���");
        SkillManager.Instance.AllOffSkill();
        _isActive = true;
    }

    private void Awake()
    {
        // ��ų ����� ���� ���ӽð� ������ �ۼ��� ��.
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
