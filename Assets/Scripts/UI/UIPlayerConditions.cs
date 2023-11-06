using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Condition
{
    [SerializeField] private Image uiBar;

    public void FillAmount(float curValue, float maxValue)
    {
        if (maxValue == 0.0f)
            return;

        uiBar.fillAmount =  (curValue / maxValue);
    }
}
public class UIPlayerConditions : MonoBehaviour
{
    private PlayerSO playerSO;

    [SerializeField] private Condition health;
    [SerializeField] private Condition kcal;

    private void Awake()
    {
        playerSO = GameManager.Instance.StatHandler.Data;
    }

    private void Update()
    {
        health.FillAmount(playerSO.Health, playerSO.MaxHealth);
        kcal.FillAmount(playerSO.Kcal, playerSO.MaxKcal);
    }

}
