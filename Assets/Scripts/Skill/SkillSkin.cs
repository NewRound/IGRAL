using UnityEngine;

public class SkillSkin : SkillUse
{
    public override void UseSkill()
    {
        if (!_isLearned)
            return;

        UIController.Instance.isSkill = false;
        Debug.Log("��Ų ���");
    }
}
