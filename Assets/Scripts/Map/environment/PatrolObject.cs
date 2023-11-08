using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolObject : MonoBehaviour, IMovingObject
{
    [Header("Movement")]
    [SerializeField] public List<Transform> destinations; // 목적지
    [SerializeField] public float moveSpeed; // 이동 속도
    [SerializeField] public float movedTime; // 편도 시간.
    [SerializeField] public float movedRateTime = 0; // 대기 시간.
    [SerializeField] public bool StartImmediately; // 즉시시작?

    private List<Vector3> Positions = new List<Vector3>();  // 목적지의 위치를 저장
    private float elapsedTime;                              // 이동이후 걸리는 시간(필요한가?)
    protected bool isMoving = false;                        // 움직이는 중인가?.
    private int destinationIndex;
    private bool isActive = false;

    private void Awake()
    {
        
    }

    private void Start()
    {
        InitializedPositions();
        if(StartImmediately)
            Use();
    }

    public void Use()
    {
        if (CheckCondition() && !isActive)
        {
            isActive = true;
            StartCoroutine(Move());
        }
    }

    public bool CheckCondition()
    {
        return true;
    }

    public void InitializedPositions()
    {
        Positions.Add(transform.position);
        for (int i = 0; i < destinations.Count; i++)
        {
            Positions.Add(destinations[i].position);
        }
        elapsedTime = 0;
        destinationIndex = 0;
    }

    public virtual IEnumerator Move()
    {
        isMoving = true;

        Vector3 startingPos = transform.position;
        Vector3 targetPos = Positions[destinationIndex];


        // 주어진 시간 동안 부드럽게 이동
        while (elapsedTime < movedTime)
        {
            transform.position = Vector3.Lerp(startingPos, targetPos, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed;
            yield return null;
        }

        destinationIndex++;

        if (destinationIndex >= Positions.Count)
            destinationIndex = 0;


        // 이동 완료 후 추가 작업 수행 가능
        elapsedTime = 0f;
        isMoving = false;
        yield return new WaitForSeconds(movedRateTime);
        StartCoroutine(Move());
    }
}
