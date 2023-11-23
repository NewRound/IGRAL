using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : CustomSingleton<GameManager>
{

    [SerializeField] private MainCam _mainCamPrefab;
    [SerializeField] private GameObject _playerObjectPrefab;

    public Camera Camera { get; private set; }
    public Transform PlayerTransform { get; private set; }
    public InputController PlayerInputController { get; private set; }
    public PlayerAppearanceController PlayerAppearanceController { get; private set; }

    public PlayerStatHandler StatHandler { get; private set; }

    public int currentStage = 1;

    public Action SceneLoad;

    public bool _isSetting { get; private set; } = false;
    public PlayerSO playerSO { get; private set; }

    public bool isTutorial = false;

    private void Awake()
    {
        //isTutorial = PlayerPrefs.GetInt("Tutorial") == 1 ? true : false;
    }

    private void Start()
    {
        SetPlayerAndCam();

        Time.timeScale = 1f;

        SceneManager.sceneLoaded += OnSceneLoaded;

        DontDestroyOnLoad(gameObject);

    }

    private void StartBGM()
    {
        AudioManager.Instance.SetStage(2);
    }

    private void SetPlayerAndCam()
    {
        GameObject playerObject = Instantiate(_playerObjectPrefab);
        MainCam mainCam = Instantiate(_mainCamPrefab);
        Camera = mainCam.GetComponentInChildren<Camera>();

        PlayerTransform = playerObject.transform;
        PlayerInputController = playerObject.GetComponent<InputController>();
        PlayerAppearanceController = playerObject.GetComponent<PlayerAppearanceController>();

        mainCam.SetMainCam();
        StatHandler = PlayerInputController.StatHandler;

        PlayerPosition(Vector3.zero);

        if (_isSetting)
            return;

        Debug.Log(UIManager.Instance);

        //Debug.Log(AudioManager.Instance);
        //Debug.Log(ItemManager.Instance);

        //임시 배경음 시작
        Invoke("StartBGM", 1f);
        //_isSetting = true;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetPlayerAndCam();

        if (SceneLoad == null)
            return;

        SceneLoad();
    }

    public void PlayerPosition(Vector3 pos)
    {
        PlayerTransform.position = pos;
    }

    public void StopGameTime()
    {
        Time.timeScale = 0f;
    }

    public void PlayGameTime()
    {
        Time.timeScale = 1f;
    }

    public void BackUpPlayerSO()
    {
        playerSO = Instantiate(StatHandler.Data);
    }

    public void PlayerAllRecovered()
    {
        StatHandler.Recovery(playerSO.MaxHealth);
        StatHandler.RecoveryKcal(playerSO.MaxKcal);
    }
}
