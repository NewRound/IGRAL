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

    private List<int> RandomBlockIndex = new List<int>();

    private Vector3 enterPosition;
    private Vector3 exitPosition;

    // 맵정보 받아오기.
    // 맵생성 시작.
    // 1. 맵 시작부분 
    private void Awake()
    {
        exitPosition = Vector3.zero;
    }

    private void Start()
    {
        foreach(GameObject block in Stage1_Blocks)
        {
            GameObject Block = Instantiate(block);
            Block.transform.position = exitPosition - enterPosition;
            exitPosition = CalculatePosition(Block, false);
            enterPosition = CalculatePosition(Block, true);
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
