public class JumpCountHandler
{
    public JumpCountHandler(int maxJumpCount)
    {
        SetJumpCount(maxJumpCount);
    }

    public int JumpCount { get; private set; }

    public void DecreaseJumpCount()
    {
        JumpCount--;
    }

    public void SetJumpCount(int count)
    {
        JumpCount = count;
    }
}