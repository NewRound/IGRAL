using UnityEngine;

public class Adrenaline : ItemConsumable
{
    float _duration = 30f;

    StatChange[] _addStats = new StatChange[3];
    StatChange[] _subtractStats = new StatChange[3];

    public override void UseConsumable()
    {
        _addStats[0] = new StatChange(StatsChangeType.Multiple, StatType.SpeedMin, 30f);
        _addStats[1] = new StatChange(StatsChangeType.Multiple, StatType.SpeedMax, 30f);
        _addStats[2] = new StatChange(StatsChangeType.Divide, StatType.AttackDelay, 30f);

        _subtractStats[0] = new StatChange(StatsChangeType.Divide, StatType.SpeedMin, 30f);
        _subtractStats[1] = new StatChange(StatsChangeType.Divide, StatType.SpeedMax, 30f);
        _subtractStats[2] = new StatChange(StatsChangeType.Multiple, StatType.AttackDelay, 30f);

        base.UseConsumable();
        SpeedChange(_duration);        
    }

    private void SpeedChange(float _duration)
    {
        GameManager.Instance.StatHandler.UpdateStats(_addStats);
        Debug.Log("속도증가");
        Invoke(nameof(SubtractSpeed), _duration);
    }

    private void SubtractSpeed()
    {
        Debug.Log("속도감소");
        GameManager.Instance.StatHandler.UpdateStats(_subtractStats);
    }
}
