using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolObject : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public List<Transform> destinations; // 목적지
    [SerializeField] public float moveSpeed; // 이동 속도
    [SerializeField] public float movedTime; // 편도 시간.

    private List<Vector3> Positions = new List<Vector3>();  // 목적지들의 위치값 저장(초기 위치 포함.)
    private float elapsedTime;                              // 이동에 걸리는 시간.
    protected bool isMoving = false;                        // 이동중인지 판단.
    private Vector3 startPosition;                          // 시작지점.
    private int destinationIndex;                           // 현재지점 파악.

    private void Awake()
    {
        // 위치값으로 저장
        Positions.Add(transform.position);
        for(int i = 0; i < destinations.Count; i++)
        {
            Positions.Add(destinations[i].position); 
        }

        //startPosition = destination.position - (destination.position - transform.position);
        destinationIndex = destinations.Count;
    }

    private void Start()
    {
        Use();
    }

    public void Use()
    {
        if (CheckCondition())
            StartCoroutine(Move());
    }

    public bool CheckCondition()
    {
        return true;
    }


    public virtual IEnumerator Move()
    {
        //Debug.Log(startPosition);
        isMoving = true;

        Vector3 startingPos = transform.position;
        //Vector3 targetPos;

        //if (direction)
        //{
        //    targetPos = destination.position;
        //}
        //else
        //{
        //    targetPos = startPosition;
        //}


        // 주어진 시간 동안 부드럽게 이동
        while (elapsedTime < movedTime)
        {
            //transform.position = Vector3.Lerp(startingPos, targetPos, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed;
            yield return null;
        }

        //direction = !direction;

        elapsedTime = 0f;
        // 이동 완료 후 추가 작업 수행 가능
        isMoving = false;
        StartCoroutine(Move());
    }
}
