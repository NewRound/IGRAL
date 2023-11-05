using UnityEngine;

public class SkillKnife : SkillUse
{
    public override void UseSkill()
    {
        if (!_isLearned)
            return;

        UIController.Instance.isSkill = false;
        Debug.Log("나이프 사용");
        _isActive = true;
    }

    private void Update()
    {
        if (_isActive)
        {
            mutantController.ChangeMutant(MutantType.Blade);
            while (true)
            {
                if (!_isActive)
                {
                    _isActive = false;
                    break;
                }
                // if(kcal <= 0) {break};
                // kcal reduce
                Debug.Log($"kcal : {-1 * Time.deltaTime}");
            }
            mutantController.ChangeMutant(MutantType.None);
        }
    }
}
