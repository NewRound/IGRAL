using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum AudioType
{
    BGMAudio
}

public class AudioManager : CustomSingleton<AudioManager>
{
    private float _bgmVolume;
    private float _sfxVolume;

    private AudioSource bgmPlayer;
    private AudioSource[] sfxPlayer;

    private void Awake()
    {
        foreach (AudioType enumItem in Enum.GetValues(typeof(AudioType)))
        {
            GameObject ui = Resources.Load<GameObject>($"Audio/{GetDescription.EnumToString(enumItem)}");
            GameObject instantiate = Instantiate(ui, Vector3.zero, Quaternion.identity);
            instantiate.transform.SetParent(this.transform);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void SetBgmVolume(float bgmVolume)
    {
        Debug.Log(bgmVolume);
        _bgmVolume = bgmVolume;
    }

    public void SetSfxVolume(float sfxVolume)
    {
        _sfxVolume = sfxVolume;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    }
}
