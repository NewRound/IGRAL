using UnityEngine;

public class SkillSkin : SkillUse
{
    public float durationTime = 5f;
    private float CurrentTime = 0f;

    public override void UseSkill()
    {
        if (!_isLearned)
            return;

        UIController.Instance.isSkill = false;
        Debug.Log("��Ų ���");
        _isActive = true;
    }

    private void Awake()
    {
        // ��ų ����� ���� ���ӽð� ������ �ۼ��� ��.
        durationTime = 5f;
    }

    private void Update()
    {
        if (_isActive)
        {
            if (mutantController.mutantType != MutantType.Skin)
                mutantController.ChangeMutant(MutantType.Skin);

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
