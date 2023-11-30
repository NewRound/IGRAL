using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Condition
{
    [SerializeField] private Image uiBar;
    [SerializeField] private TextMeshProUGUI _curValue;
    [SerializeField] private TextMeshProUGUI _maxValue;

    public void FillAmount(float curValue, float maxValue)
    {
        if (maxValue == 0.0f)
            return;

        _curValue.text = ((int)curValue).ToString();
        _maxValue.text = ((int)maxValue).ToString();

        uiBar.fillAmount =  (curValue / maxValue);
    }
}

[System.Serializable]
public class BossCondition
{
    public Image uiBar;

    public void FillAmount(float curValue, float maxValue)
    {
        if (maxValue == 0.0f)
            return;
        uiBar.fillAmount = (curValue / maxValue);
    }
}
public class UIPlayerConditions : MonoBehaviour
{
    private PlayerSO playerSO;

    [SerializeField] private Condition health;
    [SerializeField] private Condition kcal;

    private void Start()
    {
        PlayerSOSet();
        GameManager.Instance.SceneLoad += PlayerSOSet;
    }

    private void PlayerSOSet()
    {
        playerSO = GameManager.Instance.StatHandler.Data;
    }

    private void Update()
    {
        if(playerSO == null)
            return;

        health.FillAmount(playerSO.Health, playerSO.MaxHealth);
        kcal.FillAmount(playerSO.Kcal, playerSO.MaxKcal);
    }

}
