using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ObjectPoolType
{
    Drone
    , EnemyDrone
    , PlayerDroneBullet
    , EnemyDroneBullet
    , EnemyBullet
    , TurretBullet
}

public class ObjectPoolingManager : CustomSingleton<ObjectPoolingManager>
{

    [field: SerializeField] public GameObject[] prefabs { get; private set; }

    [field: SerializeField] public GameObject[] stage01 { get; private set; }
    [field: SerializeField] public GameObject[] stage02 { get; private set; }
    [field: SerializeField] public GameObject[] stage03 { get; private set; }

    private GameObject[] temp;
    private List<GameObject>[] pools;
    private List<GameObject>[] enemyPools;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (GameManager.Instance.currentStage)
        {
            case 0:
                temp = stage01;
                break;
            case 1:
                temp = stage02;
                break;
            case 2:
                temp = stage03;
                break;
        }
        if (temp == null)
            return;

        if(temp.Length > 0)
        {
            enemyPools = new List<GameObject>[temp.Length];

            for (int index = 0; index < temp.Length; index++)
            {
                enemyPools[index] = new List<GameObject>();
            }
        }
    }

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        temp = stage01;

        enemyPools = new List<GameObject>[temp.Length];

        for (int index = 0; index < temp.Length; index++)
        {
            enemyPools[index] = new List<GameObject>();
        }

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject GetGameObject(ObjectPoolType objectPoolType)
    {
        GameObject select = null;

        foreach (GameObject item in pools[(int)objectPoolType])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(prefabs[(int)objectPoolType], transform);
            pools[(int)objectPoolType].Add(select);
        }

        return select;
    }

    public GameObject GetEnemy(int index)
    {
        GameObject select = null;
        if (index > enemyPools.Length)
            index = 0;

        foreach (GameObject item in enemyPools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(temp[index], transform);
            enemyPools[index].Add(select);
        }

        return select;
    }
}
