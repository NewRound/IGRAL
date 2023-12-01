using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContainerSpawner : MonoBehaviour, IObject
{
    [SerializeField] private Transform SpawnPoint;
    [SerializeField] private GameObject LeftDoor;
    [SerializeField] private GameObject RightDoor;
    [SerializeField] private float rotateEngle;

    private bool isActive = false;
    public float speed;
    public int enemyCount;


    private List<GameObject> enemys;

    // Start is called before the first frame update
    void Start()
    {
        enemys = new List<GameObject>();
    }


    public void Use()
    {
        for(int i = 0; i < enemyCount; i++)
        {
            // 에너미 풀에서 몬스터 가져오기.
            // 에너미 위치 조정.
            // 대기.
        }

        if (!isActive)
        {
            isActive = true;
            StartCoroutine(rotateDoor(rotateEngle));
        }
        // 적들을 축으로 이동시키고 이에따라 Area에 편입.
    }

    IEnumerator rotateDoor(float engle)
    {
        float curEngle = 0.0f;

        while (true)
        {
            LeftDoor.transform.rotation *= Quaternion.Euler(0, Time.deltaTime * speed, 0);
            RightDoor.transform.rotation *= Quaternion.Euler(0, -Time.deltaTime * speed, 0);
            curEngle += Time.deltaTime * speed;

            if (curEngle > engle)
                break;

            yield return 0;
        }
    }

}
