using UnityEngine;

public class GameManager : CustomSingleton<GameManager>
{
    [field: SerializeField] public Transform PlayerTransform { get; private set; }
    [field: SerializeField] public InputController PlayerInputController { get; private set; }
    public PlayerStatHandler StatHandler { get; private set; }

    // ������Ʈ �ø� �Ŵ����� ��� �ӽ÷� ����� ��ġ��Ŵ
    [SerializeField] private GameObject droneGo;
    public Drone drone { get; private set; }

    private void Start()
    {
        StatHandler = PlayerInputController.StatHandler;

        Time.timeScale = 1f;

        Debug.Log(UIManager.Instance);
        //Debug.Log(AudioManager.Instance);
        //Debug.Log(ItemManager.Instance);

        //�ӽ� ����� ����
        Invoke("StartBGM", 1f);


        //��� �ӽ� ����
        drone = Instantiate(droneGo.GetComponent<Drone>());
        drone.gameObject.transform.parent = transform;
        drone.InActiveDrone();
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
