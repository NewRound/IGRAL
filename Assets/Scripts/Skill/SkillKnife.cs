using UnityEngine;

public class SkillKnife : SkillUse
{
    public override void UseSkill()
    {
        if (!_isLearned)
            return;

        UIController.Instance.isSkill = false;
        Debug.Log("나이프 사용");

        GameManager.Instance.player.GetComponent<PlayerAppearanceController>().ChangeMutant(MutantType.Blade);
    }
}
