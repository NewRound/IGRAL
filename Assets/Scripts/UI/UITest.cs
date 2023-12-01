using UnityEngine;

public class UITest : MonoBehaviour
{
    public void OnButton(int stage)
    {
        AudioManager.Instance.SetStage(stage);
    }

    public void OnButton2()
    {
        AudioManager.Instance.EnterBossRoom();
    }

    public void OnFootsteps()
    {
        AudioManager.Instance.PlaySFX(SFXType.Footsteps);
    }

    public void OnSwing()
    {
        AudioManager.Instance.PlaySFX(SFXType.Jump);
    }

    public void OnShooting()
    {
        AudioManager.Instance.PlaySFX(SFXType.Shooting);
    }

    public void OnDrop()
    {
        AudioManager.Instance.PlaySFX(SFXType.Drop);
    }

    public void OnPickup()
    {
        AudioManager.Instance.PlaySFX(SFXType.Pickup);
    }

}
