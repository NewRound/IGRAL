using UnityEngine;

public class GameManager : CustomSingleton<GameManager>
{
    [field: SerializeField] public Transform PlayerTransform { get; private set; }
    [field: SerializeField] public InputController PlayerInputController { get; private set; }
    public PlayerStatHandler StatHandler { get; private set; }


    private void Start()
    {
        StatHandler = PlayerInputController.StatHandler;

        Time.timeScale = 1f;

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
