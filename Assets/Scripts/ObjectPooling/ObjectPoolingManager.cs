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
    , PlayerDamagedTxt
    , EnemyDamagedTxt
    , BossShotgunBullet
}

public class ObjectPoolingManager : CustomSingleton<ObjectPoolingManager>
{

    [field: SerializeField] public GameObject[] prefabs { get; private set; }

    [field: SerializeField] public GameObject[] tutorial { get; private set; }

    private EnemyPool[] stageEnemyPool;

    private GameObject[] tempPools;
    private List<GameObject>[] pools;

    private List<GameObject>[] tutorialEnemyPools;

    private int currentStage;

    private void Awake()
    {
        currentStage = GameManager.Instance.currentStage;

        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }

        tutorialEnemyPools = new List<GameObject>[tutorial.Length];
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentStage = GameManager.Instance.currentStage;

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        if (currentStage == 0)
            return;

        EnemyPools enemyPool = Resources.Load<EnemyPools>($"Stage/Stage{currentStage}");
        tempPools = new GameObject[enemyPool.EnemyPoolPrefabs.Length];
        stageEnemyPool = enemyPool.EnemyPoolPrefabs;
        for (int i = 0; i < tempPools.Length; i++)
        {
            GameObject go = Instantiate(stageEnemyPool[i].gameObject);
            tempPools[i] = go;
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

    public EnemyController GetEnemy(int index)
    {
        EnemyController select = null;
        if (index > tempPools.Length)
            index = 0;

        EnemyPool enemyPool = tempPools[index].GetComponent<EnemyPool>();

        select = enemyPool.GetObject();

        return select;
    }

    public GameObject GetEnemyTutorial(int index)
    {

        for (int i = 0; i < tutorial.Length; i++)
        {
            tutorialEnemyPools[i] = new List<GameObject>();
        }

        GameObject select = null;

        foreach (GameObject item in tutorialEnemyPools[index])
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
            select = Instantiate(tutorial[index], transform);
            tutorialEnemyPools[index].Add(select);
        }

        return select;
    }
}
