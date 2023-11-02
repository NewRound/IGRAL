using System;
using System.Collections.Generic;

public class SkillManager : CustomSingleton<SkillManager>
{
    public int skillPoint = 5;
    public Dictionary<string, int> learnedSkills = new Dictionary<string, int>();


}
