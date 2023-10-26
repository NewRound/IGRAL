public class PlayerStatHandler : CharacterStatHandler
{
    public PlayerSO Data { get; private set; }

    public PlayerStatHandler(PlayerSO data)
    {
        Data = data;
    }
}
