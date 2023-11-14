public class JumpCountHandler
{
    public int JumpCount { get; private set; }
    private int _jumpCountMax;

    public JumpCountHandler(int jumpCountMax)
    {
        SetJumpCountMax(jumpCountMax);
    }

    public void IncreaseJumpCount()
    {
        JumpCount = JumpCount > _jumpCountMax ? _jumpCountMax : ++JumpCount;
    }

    public void DecreaseJumpCount()
    {
        JumpCount--;
    }

    public void ResetJumpCount()
    {
        JumpCount = _jumpCountMax;
    }

    public void SetJumpCountMax(int count)
    {
        _jumpCountMax = count;
        ResetJumpCount();
    }
}