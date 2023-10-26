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

    [SerializeField] public int tileSize;

    private GameObject frontBlock = null;

    private List<int> RandomBlockIndex = new List<int>();

    // 맵정보 받아오기.
    // 맵생성 시작.
    // 1. 맵 시작부분 
    private void Awake()
    {

    }

    private void Start()
    {
        foreach(GameObject block in Stage1_Blocks)
        {
            GameObject Block = Instantiate(block);

            if(frontBlock != null)
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
            //Block.transform.position = exitPosition - enterPosition;
            //exitPosition = CalculatePosition(Block, false);
            //enterPosition = CalculatePosition(Block, true);
            //Debug.Log(enterPosition);
            //Debug.Log(exitPosition);
        }
    }



    // 포지션 계산 함수.
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
}
