public class EnemyStatHandler : CharacterStatHandler
{
    public EnemySO Data { get; private set; }

    public EnemyStatHandler(EnemySO data)
    {
        Data = data;
    }
}
