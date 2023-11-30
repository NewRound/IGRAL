using UnityEngine;

public class UIBossCondition : MonoBehaviour
{

    [SerializeField] private BossCondition hp;
    [SerializeField] private BossCondition action;

    private float MaxHP;
    private float MaxAction;

    private void OnEnable()
    {
        ChangePhase(1);
    }

    public void SetMaxHP(float maxHP)
    {
        MaxHP = maxHP;
    }

    public void SetMaxAction(float maxAction)
    {
        MaxAction = maxAction;
    }

    public void DisplayHP(float currentHP)
    {
        hp.FillAmount(currentHP, MaxHP);
    }

    public void DisplayAction(float currentAction)
    {
        action.FillAmount(currentAction, MaxAction);
    }

    public void ChangePhase(int phase)
    {
        switch (phase)
        {
            case 1:
                action.uiBar.color = new Color(247 / 255f, 220 / 255f, 111 / 255f);
                break; 
            case 2:
                action.uiBar.color = new Color(212 / 255f, 172 / 255f, 13 / 255f);
                break;
            case 3:
                action.uiBar.color = new Color(220 / 255f, 118 / 255f, 51 / 255f);
                break;
        }
    }
}
