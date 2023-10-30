using UnityEngine;
using UnityEngine.UI;

public class CommonOption : MonoBehaviour
{
    [SerializeField] private Slider _bgmVolume;
    [SerializeField] private Slider _sfxVolume;
    [SerializeField] private Button _exitButton;

    private void Awake()
    {
        _bgmVolume.onValueChanged.AddListener(UpdateBgmVolume);
        _sfxVolume.onValueChanged.AddListener(UpdateSfxVolume);
        _exitButton.onClick.AddListener(OnExitButton);
    }

    private void Start()
    {
        InitAudio();
    }

    private void InitAudio()
    {
        _bgmVolume.value = PlayerPrefs.GetFloat("bgmVolume");
        _sfxVolume.value = PlayerPrefs.GetFloat("sfxVolume");

        AudioManager.Instance.SetBgmVolume(_bgmVolume.value);
        AudioManager.Instance.SetSfxVolume(_sfxVolume.value);
    }

    private void UpdateBgmVolume(float volume)
    {
        AudioManager.Instance.SetBgmVolume(volume);
        PlayerPrefs.SetFloat("bgmVolume", volume);
    }

    private void UpdateSfxVolume(float volume)
    {
        AudioManager.Instance.SetSfxVolume(volume);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    private void OnExitButton()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

}
