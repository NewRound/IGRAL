using UnityEngine;

public class SkillHammer : SkillUse
{
    public override void UseSkill()
    {
        if (!_isLearned)
            return;

        UIController.Instance.isSkill = false;
        Debug.Log("�ظ� ���");

        GameManager.Instance.player.GetComponent<PlayerAppearanceController>().ChangeMutant(MutantType.Stone);
    }
}
