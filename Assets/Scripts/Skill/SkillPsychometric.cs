using UnityEngine;

public class SkillPsychometric : SkillUse
{
    public override void UseSkill()
    {
        if (!_isLearned)
            return;

        UIController.Instance.isSkill = false;
        Debug.Log("사이코 메트릭 사용");
    }
}
