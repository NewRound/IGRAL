using UnityEngine;

public class SkillPsychometric : SkillUse
{
    public override void UseSkill()
    {
        base.UseSkill();
        Debug.Log("사이코 메트릭 사용");
    }
}
