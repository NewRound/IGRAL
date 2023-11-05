using UnityEngine;

public class SkillSkin : SkillUse
{
    public float durationTime = 5000f;
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
        durationTime = 5000f;
    }

    private void Update()
    {
        if (_isActive)
        {
            mutantController.ChangeMutant(MutantType.Skin);
            while (true)
            {
                if (!_isActive || CurrentTime >= durationTime)
                {
                    CurrentTime = 0f;
                    _isActive = false;
                    break;
                }

                CurrentTime += Time.deltaTime;
                // if(kcal <= 0) {break};
                // kcal reduce
                Debug.Log($"kcal : {CurrentTime}/{durationTime},    {-1 * Time.deltaTime}");
            }
            mutantController.ChangeMutant(MutantType.None);
        }
    }
}
