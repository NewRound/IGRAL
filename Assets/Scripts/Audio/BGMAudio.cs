using UnityEngine;

public class BGMAudio : MonoBehaviour
{
    [Header("Stage")]
    [SerializeField] private AudioClip[] _stageBGM;
    [SerializeField] private AudioClip[] _bossBGM;

    public AudioClip[] GetStageBGM(int stage)
    {
        AudioClip[] re = new AudioClip[2];
        re[0] = _stageBGM[stage];
        re[1] = _stageBGM[stage];
        return re;
    }

}
