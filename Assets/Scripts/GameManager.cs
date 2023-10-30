using UnityEngine;

public class GameManager : CustomSingleton<GameManager>
{
    public GameObject player;

    private void Awake()
    {
        Debug.Log(UIManager.Instance);
        Debug.Log(AudioManager.Instance);
    }

    private void Start()
    {
        if(!PlayerPrefs.HasKey("bgmVolume"))
        {
            PlayerPrefs.SetFloat("bgmVolume", 1.0f);
            PlayerPrefs.SetFloat("sfxVolume", 1.0f);
        }
    }

}
