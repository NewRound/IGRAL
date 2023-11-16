using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : CustomSingleton<GameManager>
{

    [field: SerializeField] public Camera Camera { get; private set; }
    [field: SerializeField] public Transform PlayerTransform { get; private set; }
    [field: SerializeField] public InputController PlayerInputController { get; private set; }
    public PlayerStatHandler StatHandler { get; private set; }

    public int currentStage = 1;

    private void Start()
    {
        StatHandler = PlayerInputController.StatHandler;

        Time.timeScale = 1f;

        Debug.Log(UIManager.Instance);
        //Debug.Log(AudioManager.Instance);
        //Debug.Log(ItemManager.Instance);

        //임시 배경음 시작
        Invoke("StartBGM", 1f);
        SceneManager.sceneLoaded += OnSceneLoaded;

        DontDestroyOnLoad(gameObject);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayerTransform.position = Vector3.zero;
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
