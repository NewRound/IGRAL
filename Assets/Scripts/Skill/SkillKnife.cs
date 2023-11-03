using UnityEngine;

public class SkillKnife : SkillUse
{
    public override void UseSkill()
    {
        if (!_isLearned)
            return;

        UIController.Instance.isSkill = false;
        Debug.Log("������ ���");

        GameManager.Instance.player.GetComponent<PlayerAppearanceController>().ChangeMutant(MutantType.Blade);
    }
}
