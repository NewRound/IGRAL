
public class Grenade : ItemConsumable
{
    public override void UseConsumable()
    {
        base.UseConsumable();

        // TODO
        // 전방으로 투척하여 일정시간 후에 데미지를 주고 사라지기
        // 물리 움직임, 투척 포물선, 적 충돌 감지 콜라이더?
    }
}
