using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : CustomSingleton<GameManager>
{
    [field: SerializeField] public Transform PlayerTransform { get; private set; }
    [field: SerializeField] public InputController PlayerInputController { get; private set; }
    public PlayerStatHandler StatHandler { get; private set; }

    // 오브젝트 플링 매니저가 없어서 임시로 드론을 위치시킴
    [SerializeField] private GameObject droneGo;
    public Drone drone { get; private set; }

    private void Start()
    {
        StatHandler = PlayerInputController.StatHandler;

        Time.timeScale = 1f;

        Debug.Log(UIManager.Instance);
        //Debug.Log(AudioManager.Instance);
        //Debug.Log(ItemManager.Instance);

        //임시 배경음 시작
        Invoke("StartBGM", 1f);


        //드론 임시 세팅
        drone = Instantiate(droneGo.GetComponent<Drone>());
        drone.gameObject.transform.parent = transform;
        drone.InActiveDrone();

        DontDestroyOnLoad(gameObject);
        // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
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
