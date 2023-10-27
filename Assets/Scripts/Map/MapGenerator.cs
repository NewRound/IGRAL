using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

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
        
        // 랜덤 생성 TODO

        foreach (GameObject block in createStage)
        {
            GameObject Block = Instantiate(block);

            if (frontBlock != null)
            {
                Vector3 _position = Block.transform.position;
                Vector3 _frontPosition = frontBlock.transform.position;
                MapData _mapData = frontBlock.GetComponent<MapData>();

                _position.x = _frontPosition.x + (_mapData.horizontalOfTiles * tileSize);
                _position.y = _frontPosition.y + (_mapData.verticalOfTiles * tileSize);
                Block.transform.position = _position;
            }
            else
            {
                Vector3 _position = Block.transform.position;
                _position.x = 0;
                _position.y = 0;
                Block.transform.position = _position;
            }

            frontBlock = Block;
        }
        currentStage++;
    }
    


    // 포지션 계산 함수.
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
