using System.Collections;
using UnityEngine;

public class ElevatorObject : MonoBehaviour, IMovingObject
{
    private Vector3 StartPosition;

    [SerializeField] public Transform Destination;
    [SerializeField] public float moveSpeed;

    private float elapsedTime = 0;                              // �̵����� �ɸ��� �ð�(�ʿ��Ѱ�?)
    protected bool isMoving = false;                        // �����̴� ���ΰ�?.

    private void Awake()
    {
        StartPosition = transform.position;
        Debug.Log($"{StartPosition}");
    }

    public void Use()
    {
        if (!isMoving)
        {
            Debug.Log("isMoving = false");
            Debug.Log($"{transform.position}, {StartPosition}, {Destination.position}");
            Debug.Log($"{Vector3.Distance(transform.position, Destination.position)}, {Vector3.Distance(transform.position, StartPosition)}");

            if (Vector3.Distance(transform.position, Destination.position) < 0.1)
            {
                Debug.Log("go to StartPosition");
                StartCoroutine(Move(transform.position, StartPosition));
            }
            else if (Vector3.Distance(transform.position, StartPosition) < 0.1)
            {
                Debug.Log("go to destination");
                StartCoroutine(Move(transform.position, Destination.position));
            }
            else
            {
                StartCoroutine(Move(transform.position, Destination.position));
            }
        }
    }

    IEnumerator Move(Vector3 startingPos, Vector3 targetPos)
    {
        isMoving = true;

        float durationTime = Vector3.Distance(startingPos, targetPos);

        // �־��� �ð� ���� �ε巴�� �̵�
        while (elapsedTime < durationTime)
        {
            transform.position = Vector3.Lerp(startingPos, targetPos, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed;
            Debug.Log("Move");
            yield return null;
        }

        // �̵� �Ϸ� �� �߰� �۾� ���� ����
        elapsedTime = 0f;
        isMoving = false;
    }

    public bool CheckCondition()
    {
        return true;
    }
}
