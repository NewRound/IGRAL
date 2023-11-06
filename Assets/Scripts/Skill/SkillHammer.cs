using UnityEngine;

public class SkillHammer : SkillUse
{
    public override void UseSkill()
    {
        if (!_isLearned)
            return;

        UIController.Instance.isSkill = false;
        Debug.Log("해머 사용");
        _isActive = true;
    }

    private void Update()
    {
        if(_isActive)
        {
            if (mutantController.mutantType != MutantType.Stone)
                mutantController.ChangeMutant(MutantType.Stone);

            // if(kcal <= 0) {break};
            // kcal reduce
            Debug.Log($"kcal : { -1 * Time.deltaTime}");
        }
        else
        {
            StopSkill();
        }
    }
}
