using UnityEngine;

public class SkillKnife : SkillUse
{
    public override void UseSkill()
    {
        if (!_isLearned)
            return;

        UIController.Instance.isSkill = false;
        Debug.Log("������ ���");
    }
}
