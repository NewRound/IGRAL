using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] public List<GameObject> Stage1_Blocks;
    [SerializeField] public List<GameObject> Stage2_Blocks;
    [SerializeField] public List<GameObject> Stage3_Blocks;

    [SerializeField] public int numberOfBlock;
    [SerializeField] public int lastStage = 3;
    [SerializeField] public int tileSize;

    private GameObject frontBlock = null;

    private List<int> RandomBlockIndex = new List<int>();

    private int currentStage;

    private void Awake()
    {

    }

    private void Start()
    {
        //foreach(GameObject block in Stage1_Blocks)
        //{
        //    GameObject Block = Instantiate(block);

        //    if(frontBlock != null)
        //    {
        //        Vector3 _position = Block.transform.position;
        //        Vector3 _frontPosition = frontBlock.transform.position;
        //        MapData _mapData = frontBlock.GetComponent<MapData>();

        //        _position.x = _frontPosition.x + (_mapData.horizontalOfTiles * tileSize);
        //        _position.y = _frontPosition.y + (_mapData.verticalOfTiles * tileSize);
        //        Block.transform.position = _position;
        //    }
        //    else
        //    {
        //        Vector3 _position = Block.transform.position;
        //        _position.x = 0;
        //        _position.y = 0;
        //        Block.transform.position = _position;
        //    }

        //    frontBlock = Block;
        //}
        //currentStage = 1;
        InstantiateStage();
    }

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
