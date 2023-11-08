using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolObject : MonoBehaviour, IMovingObject
{
    [Header("Movement")]
    [SerializeField] public List<Transform> destinations; // ������
    [SerializeField] public float moveSpeed; // �̵� �ӵ�
    [SerializeField] public float movedTime; // �� �ð�.
    [SerializeField] public float movedRateTime = 0; // ��� �ð�.
    [SerializeField] public bool StartImmediately; // ��ý���?

    private List<Vector3> Positions = new List<Vector3>();  // �������� ��ġ�� ����
    private float elapsedTime;                              // �̵����� �ɸ��� �ð�(�ʿ��Ѱ�?)
    protected bool isMoving = false;                        // �����̴� ���ΰ�?.
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


        // �־��� �ð� ���� �ε巴�� �̵�
        while (elapsedTime < movedTime)
        {
            transform.position = Vector3.Lerp(startingPos, targetPos, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed;
            yield return null;
        }

        destinationIndex++;

        if (destinationIndex >= Positions.Count)
            destinationIndex = 0;


        // �̵� �Ϸ� �� �߰� �۾� ���� ����
        elapsedTime = 0f;
        isMoving = false;
        yield return new WaitForSeconds(movedRateTime);
        StartCoroutine(Move());
    }
}
