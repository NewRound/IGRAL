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

    private void Start()
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
