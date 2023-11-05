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
            mutantController.ChangeMutant(MutantType.Stone);
            while (true)
            {
                if (!_isActive)
                {
                    _isActive = false;
                    break;
                }
                // if(kcal <= 0) {break};
                // kcal reduce
                Debug.Log($"kcal : { -1 * Time.deltaTime}");
            }
            mutantController.ChangeMutant(MutantType.None);
        }
    }

}
