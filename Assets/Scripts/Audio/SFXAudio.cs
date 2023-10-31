using UnityEngine;

public class SFXAudio : MonoBehaviour
{
    [SerializeField] private AudioClip[] _sfx;

    public AudioClip[] GetSFX()
    {
        return _sfx;
    }
}
