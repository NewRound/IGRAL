using UnityEngine;

public class SkillPsychometric : SkillUse
{
    public override void UseSkill()
    {
        if (!_isLearned)
            return;

        UIController.Instance.isSkill = false;
        Debug.Log("������ ��Ʈ�� ���");
    }
}
