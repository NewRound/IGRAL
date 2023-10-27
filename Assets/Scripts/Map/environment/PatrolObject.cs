using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolObject : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public List<Transform> destinations; // ������
    [SerializeField] public float moveSpeed; // �̵� �ӵ�
    [SerializeField] public float movedTime; // �� �ð�.

    private List<Vector3> Positions = new List<Vector3>();  // ���������� ��ġ�� ����(�ʱ� ��ġ ����.)
    private float elapsedTime;                              // �̵��� �ɸ��� �ð�.
    protected bool isMoving = false;                        // �̵������� �Ǵ�.
    private Vector3 startPosition;                          // ��������.
    private int destinationIndex;                           // �������� �ľ�.

    private void Awake()
    {
        // ��ġ������ ����
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


        // �־��� �ð� ���� �ε巴�� �̵�
        while (elapsedTime < movedTime)
        {
            //transform.position = Vector3.Lerp(startingPos, targetPos, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed;
            yield return null;
        }

        //direction = !direction;

        elapsedTime = 0f;
        // �̵� �Ϸ� �� �߰� �۾� ���� ����
        isMoving = false;
        StartCoroutine(Move());
    }
}
