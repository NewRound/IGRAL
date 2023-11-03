using UnityEngine;

public class SkillSkin : SkillUse
{
    public override void UseSkill()
    {
        base.UseSkill();
        Debug.Log("스킨 사용");
    }
}
