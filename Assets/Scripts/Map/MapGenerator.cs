using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator Instance;

    [SerializeField] public List<GameObject> Stage1_mapBlocks;
    [SerializeField] public List<GameObject> Stage2_mapBlocks;
    [SerializeField] public List<GameObject> Stage3_mapBlocks;

    [SerializeField] public int numberOfmapBlock;
    [SerializeField] public int lastStage = 3;
    [SerializeField] public int tileSize;

    private GameObject frontmapBlock = null;

    private List<int> RandommapBlockIndex = new List<int>();

    private int currentStage;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        currentStage = GameManager.Instance.currentStage;
    }

    private void Start()
    {
        // 개임메니저로 옮기기.
        InstantiateStage();
    }


    // 맵을 넘어가기 위한 상호작용 오브젝트에서 호출 할 것.
    public void InstantiateStage()
    {
        if (currentStage >= lastStage)
            return;

        List<GameObject> createStage;

        switch(currentStage)
        {
            case 0:
                createStage = Stage1_mapBlocks;
                break;
            case 1:
                createStage = Stage2_mapBlocks;
                break;
            case 2:
                createStage = Stage3_mapBlocks;
                break;
            default:
                createStage = null;
                break;
        }

        if(createStage == null || createStage.Count == 0)
            return;

        // 랜덤 생성 TODO
        if (createStage.Count - 2 < numberOfmapBlock)
        {
            int randomValue;

            while (RandommapBlockIndex.Count != createStage.Count - 2)
            {
                randomValue = Random.Range(1, createStage.Count - 1);

                if (!RandommapBlockIndex.Contains(randomValue))
                {
                    RandommapBlockIndex.Add(randomValue);
                }
            }
        }
        else
        {
            int randomValue;

            while (RandommapBlockIndex.Count != numberOfmapBlock)
            {
                randomValue = Random.Range(1, createStage.Count - 1);

                if (!RandommapBlockIndex.Contains(randomValue))
                {
                    RandommapBlockIndex.Add(randomValue);
                }
            }
        }


        // 첫맵 생성
        CreateBlock(createStage, 0);

        for (int i = 0; i < RandommapBlockIndex.Count; i++)
        {
            CreateBlock(createStage, RandommapBlockIndex[i]);
        }

        // 마지막 맵 생성
        CreateBlock(createStage, createStage.Count - 1);

        currentStage++;
    }
    

    private void CreateBlock(List<GameObject> _createStage, int createIndex)
    {
        GameObject newBlock = Instantiate(_createStage[createIndex]);
        Vector3 _position = newBlock.transform.position;
        if (frontmapBlock != null)
        {
            Vector3 _frontPosition = frontmapBlock.transform.position;
            MapData _mapData = frontmapBlock.GetComponent<MapData>();
            _position.x = _frontPosition.x + (_mapData.horizontalOfTiles * tileSize);
            _position.y = _frontPosition.y + (_mapData.verticalOfTiles * tileSize);
        }
        else
        {
            _position.x = 0;
            _position.y = 0;
        }
        newBlock.transform.position = _position;
        frontmapBlock = newBlock;
    }


    // 포지션 계산 함수.
    /*
    private Vector3 CalculatePosition(GameObject mapBlockPrefab, bool isEnterPosition)
    {
        if(isEnterPosition)
        {
            Transform value = mapBlockPrefab.transform.Find("EnterPosition");

            if (value != null)
            {
                Vector3 answer = mapBlockPrefab.transform.position + value.position;

                return answer;
            }
        }
        else
        {
            Transform value = mapBlockPrefab.transform.Find("ExitPosition");

            if (value != null)
            {
                Vector3 answer = mapBlockPrefab.transform.position + value.position;

                return answer;
            }
        }

        return Vector3.zero;
    }
    */
}
