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
            // ���ʹ� Ǯ���� ���� ��������.
            // ���ʹ� ��ġ ����.
            // ���.
        }

        if (!isActive)
        {
            isActive = true;
            StartCoroutine(rotateDoor(rotateEngle));
        }
        // ������ ������ �̵���Ű�� �̿����� Area�� ����.
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
