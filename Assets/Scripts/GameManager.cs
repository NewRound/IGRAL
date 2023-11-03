using UnityEngine;

public class GameManager : CustomSingleton<GameManager>
{
    public GameObject player { get; private set; }
    public PlayerStatHandler statHandler { get; private set; }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //statHandler = player.GetComponent<PlayerStatHandler>();

        Debug.Log(UIManager.Instance);
        Debug.Log(AudioManager.Instance);
        Debug.Log(ItemManager.Instance);
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
