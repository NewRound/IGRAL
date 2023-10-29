using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Unity.Collections.AllocatorManager;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator Instance;

    [SerializeField] public List<GameObject> Stage1_Blocks;
    [SerializeField] public List<GameObject> Stage2_Blocks;
    [SerializeField] public List<GameObject> Stage3_Blocks;

    [SerializeField] public int numberOfBlock;
    [SerializeField] public int lastStage = 3;
    [SerializeField] public int tileSize;

    private GameObject frontBlock = null;

    private List<int> RandomBlockIndex = new List<int>();

    private int currentStage = 0;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        // ���Ӹ޴����� �ű��.
        InstantiateStage();
    }


    // ���� �Ѿ�� ���� ��ȣ�ۿ� ������Ʈ���� ȣ�� �� ��.
    public void InstantiateStage()
    {
        if (currentStage >= lastStage)
            return;

        List<GameObject> createStage;

        switch(currentStage)
        {
            case 0:
                createStage = Stage1_Blocks;
                break;
            case 1:
                createStage = Stage2_Blocks;
                break;
            case 2:
                createStage = Stage3_Blocks;
                break;
            default:
                createStage = null;
                break;
        }

        if(createStage == null || createStage.Count == 0)
            return;

        Debug.Log($"{numberOfBlock}, {createStage.Count - 2} ");

        // ���� ���� TODO
        if (createStage.Count - 2 < numberOfBlock)
        {
            int randomValue;
            Debug.Log($"{RandomBlockIndex.Count}, {createStage.Count - 2} ");

            while (RandomBlockIndex.Count != createStage.Count - 2)
            {
                randomValue = Random.Range(1, createStage.Count - 1);

                if (!RandomBlockIndex.Contains(randomValue))
                {
                    RandomBlockIndex.Add(randomValue);
                    Debug.Log(randomValue);
                }
            }
        }
        else
        {
            int randomValue;
            

            while (RandomBlockIndex.Count != numberOfBlock)
            {
                randomValue = Random.Range(1, createStage.Count - 1);

                if (!RandomBlockIndex.Contains(randomValue))
                {
                    RandomBlockIndex.Add(randomValue);
                }
            }
        }


        // ù�� ����
        CreateBlock(createStage, 0);

        for (int i = 0; i < RandomBlockIndex.Count; i++)
        {
            CreateBlock(createStage, RandomBlockIndex[i]);
        }

        // ������ �� ����
        CreateBlock(createStage, createStage.Count - 1);

        currentStage++;
    }
    

    private void CreateBlock(List<GameObject> _createStage, int createIndex)
    {
        GameObject Block = Instantiate(_createStage[createIndex]);
        Vector3 _position = Block.transform.position;
        if (frontBlock != null)
        {
            Vector3 _frontPosition = frontBlock.transform.position;
            MapData _mapData = frontBlock.GetComponent<MapData>();
            _position.x = _frontPosition.x + (_mapData.horizontalOfTiles * tileSize);
            _position.y = _frontPosition.y + (_mapData.verticalOfTiles * tileSize);
        }
        else
        {
            _position.x = 0;
            _position.y = 0;
        }
        Block.transform.position = _position;
        frontBlock = Block;
    }


    // ������ ��� �Լ�.
    /*
    private Vector3 CalculatePosition(GameObject blockPrefab, bool isEnterPosition)
    {
        if(isEnterPosition)
        {
            Transform value = blockPrefab.transform.Find("EnterPosition");

            if (value != null)
            {
                Vector3 answer = blockPrefab.transform.position + value.position;

                return answer;
            }
        }
        else
        {
            Transform value = blockPrefab.transform.Find("ExitPosition");

            if (value != null)
            {
                Vector3 answer = blockPrefab.transform.position + value.position;

                return answer;
            }
        }

        return Vector3.zero;
    }
    */
}
