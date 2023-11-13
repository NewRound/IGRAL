
using System.Collections;
using UnityEngine;

public class Adrenaline : ItemConsumable
{
    public override void UseConsumable()
    {
        base.UseConsumable();

        // �÷��̾��� �̵�,���� �ӵ� ���� �ð� ���� ���� �ڷ�ƾ?
        StartCoroutine(nameof(SpeedUp));
    }

    private IEnumerator SpeedUp()
    {
        yield return new WaitForSeconds(1);
    }
}
