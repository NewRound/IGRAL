
using System.Collections;
using UnityEngine;

public class Adrenaline : ItemConsumable
{
    public override void UseConsumable()
    {
        base.UseConsumable();

        // 플레이어의 이동,공격 속도 일정 시간 동안 증가 코루틴?
        StartCoroutine(nameof(SpeedUp));
    }

    private IEnumerator SpeedUp()
    {
        yield return new WaitForSeconds(1);
    }
}
