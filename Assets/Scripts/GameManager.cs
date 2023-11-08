using UnityEngine;

public class GameManager : CustomSingleton<GameManager>
{
    public GameObject player { get; private set; }
    public PlayerStatHandler StatHandler { get; private set; }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StatHandler = player.GetComponent<InputController>().StatHandler;

        Time.timeScale = 1f;
        if (!PlayerPrefs.HasKey("bgmVolume"))
        {
            PlayerPrefs.SetFloat("bgmVolume", 1.0f);
            PlayerPrefs.SetFloat("sfxVolume", 1.0f);
        }

        Debug.Log(UIManager.Instance);
        Debug.Log(AudioManager.Instance);
        Debug.Log(ItemManager.Instance);

        //임시 배경음 시작
        Invoke("StartBGM", 1f);
    }

    private void StartBGM()
    {
        AudioManager.Instance.SetStage(2);
    }


    public void StopGameTime()
    {
        Time.timeScale = 0f;
    }

    public void PlayGameTime()
    {
        Time.timeScale = 1f;
    }
}
