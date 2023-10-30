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

    private void UpdateBgmVolume(float volume)
    {
        AudioManager.Instance.SetBgmVolume(volume);
    }

    private void UpdateSfxVolume(float volume)
    {
        AudioManager.Instance.SetSfxVolume(volume);
    }

    private void OnExitButton()
    {
        Application.Quit();
    }

}
