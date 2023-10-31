using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum AudioType
{
    BGMAudio
    , SFXAudio
}

public enum SFXType
{
    Footsteps
    , Swing
    , Shooting
    , Drop
    , Pickup

    , Max
}

public class AudioManager : CustomSingleton<AudioManager>
{
    private BGMAudio _bgmAudio;
    private SFXAudio _sfxAudio;

    private AudioClip[] _bgm;
    private AudioClip[] _sfx;

    private AudioSource bgmPlayer;
    private AudioSource[] sfxPlayer;

    private void Awake()
    {
        bgmPlayer = gameObject.AddComponent<AudioSource>();
        bgmPlayer.loop = true;
        bgmPlayer.volume = PlayerPrefs.GetFloat("bgmVolume");

        sfxPlayer = new AudioSource[(int)SFXType.Max];
        for (int i = 0; i < (int)SFXType.Max; i++)
        {
            sfxPlayer[i] = gameObject.AddComponent<AudioSource>();
            sfxPlayer[i].loop = false;
            sfxPlayer[i].volume = PlayerPrefs.GetFloat("sfxVolume");
        }

        foreach (AudioType enumItem in Enum.GetValues(typeof(AudioType)))
        {
            GameObject ui = Resources.Load<GameObject>($"Audio/{GetDescription.EnumToString(enumItem)}");
            GameObject instantiate = Instantiate(ui, Vector3.zero, Quaternion.identity);
            instantiate.transform.SetParent(this.transform);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        _bgmAudio = gameObject.GetComponentInChildren<BGMAudio>();
        _sfxAudio = gameObject.GetComponentInChildren<SFXAudio>();

        _sfx = _sfxAudio.GetSFX();
        for (int i = 0; i < (int)SFXType.Max; i++)
        {
            sfxPlayer[i].clip = _sfx[i];
        }
    }

    public void SetBgmVolume(float bgmVolume)
    {
        bgmPlayer.volume = bgmVolume;
    }

    public void SetSfxVolume(float sfxVolume)
    {
        for (int i = 0; i < (int)SFXType.Max; i++)
        {
            sfxPlayer[i].volume = sfxVolume;
        }
    }

    public void SetStage(int  stage)
    {
        _bgm = _bgmAudio.GetStageBGM(stage);

        bgmPlayer.clip = _bgm[0];

        bgmPlayer.Play();
    }

    public void EnterBossRoom()
    {
        bgmPlayer.clip = _bgm[1];

        bgmPlayer.Play();
    }

    public void PlaySFX(SFXType sfxType)
    {
        sfxPlayer[(int)sfxType].Play();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    }
}