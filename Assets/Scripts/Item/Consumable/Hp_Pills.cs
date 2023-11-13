
public class Hp_Pills : ItemConsumable
{
    private float _value;

    private void Awake()
    {
        _value = GameManager.Instance.PlayerInputController.StatHandler.Data.MaxHealth * 0.05f;
    }

    public override void UseConsumable()
    {
        base.UseConsumable();

        // TODO
        // 플레이어의 현재 체력을 Value 만큼 회복

        GameManager.Instance.PlayerInputController.StatHandler.Recovery(_value);
    }
}
