using UnityEngine;

public class SkillKnife : SkillUse
{
    public override void UseSkill()
    {
        base.UseSkill();
        Debug.Log("������ ���");
    }
}
