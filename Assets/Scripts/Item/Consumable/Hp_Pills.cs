using UnityEngine;

public class Hp_Pills : ItemConsumable
{
    private float _value;

    public override void UseConsumable()
    {
        base.UseConsumable();

        _value = GameManager.Instance.StatHandler.Data.MaxHealth * 0.05f;
        GameManager.Instance.StatHandler.Recovery(_value);
    }
}
