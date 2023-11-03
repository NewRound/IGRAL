using UnityEngine;

public class SkillHammer : SkillUse
{
    public override void UseSkill()
    {
        base.UseSkill();
        Debug.Log("해머 사용");
    }
}
