using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyContainerSpawner : MonoBehaviour, IObject
{
    [SerializeField] private Transform SpawnPoint;
    [SerializeField] private GameObject LeftDoor;
    [SerializeField] private GameObject RightDoor;
    [SerializeField] private float rotateEngle;

    private bool isActive = false;
    private bool isOpen = false;
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
            GameObject enemy = ObjectPoolingManager.Instance.GetEnemy(0).gameObject;
            enemy.transform.position = transform.position;
            enemy.GetComponent<EnemyController>().StateMachine.SetDirection(Vector3.zero);
            enemys.Add(enemy);
        }

        if (!isActive)
        {
            isActive = true;
            StartCoroutine(rotateDoor(rotateEngle));
        }

        if(isOpen)
        {
            // 적들을 축으로 이동시키고 이에따라 Area에 편입.
            foreach(GameObject enemy in enemys)
            {
                enemy.GetComponent<EnemyController>().StateMachine.SetDirection(Vector3.zero);
            }
        }
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
        isOpen = true;
    }

}
