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
            if(mutantController.mutantType != MutantType.Blade)
                mutantController.ChangeMutant(MutantType.Blade);
            
            // if(kcal <= 0) {break};
            // kcal reduce
            Debug.Log($"kcal : {-1 * Time.deltaTime}");
        }
        else
        {
            StopSkill();
        }
    }
}
