public class DataManager : CustomSingleton<DataManager>
{
    public PlayerSO playerSO { get; private set; }

    public void BackUpPlayerSO()
    {
        playerSO = Instantiate(GameManager.Instance.StatHandler.Data);
    }
}
