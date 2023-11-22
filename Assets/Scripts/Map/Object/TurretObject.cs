using UnityEngine;
using UnityEngine.XR;

public class TurretObject : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float delayTime;
    private ObjectPoolingManager poolingManager;
    public Vector3 Direction;
    private float curTime;
    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        poolingManager = ObjectPoolingManager.Instance;
        curTime = 0;
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (curTime > delayTime)
            {
                curTime = 0;
                GameObject bullet = poolingManager.GetGameObject(ObjectPoolType.TurretBullet);
                bullet.transform.position = spawnPoint.position;
                bullet.GetComponent<TurretBullet>().SetDirection(Direction);
            }
            else
            {
                curTime += Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        InputController inputController = other.GetComponent<InputController>();
        if (inputController != null)
        {
            isActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        InputController inputController = other.GetComponent<InputController>();
        if (inputController != null)
        {
            isActive = false;
        }
    }
}