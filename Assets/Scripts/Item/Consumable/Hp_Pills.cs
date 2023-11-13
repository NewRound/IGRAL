
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
        // �÷��̾��� ���� ü���� Value ��ŭ ȸ��

        GameManager.Instance.PlayerInputController.StatHandler.Recovery(_value);
    }
}
