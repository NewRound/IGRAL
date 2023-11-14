using System.Collections;
using UnityEngine;

public class ElevatorObject : MonoBehaviour, IMovingObject
{
    private Vector3 StartPosition;

    [SerializeField] public Transform Destination;
    [SerializeField] public float moveSpeed;

    private float elapsedTime = 0;                              // 이동이후 걸리는 시간(필요한가?)
    protected bool isMoving = false;                        // 움직이는 중인가?.

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

        // 주어진 시간 동안 부드럽게 이동
        while (elapsedTime < durationTime)
        {
            transform.position = Vector3.Lerp(startingPos, targetPos, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed;
            Debug.Log("Move");
            yield return null;
        }

        // 이동 완료 후 추가 작업 수행 가능
        elapsedTime = 0f;
        isMoving = false;
    }

    public bool CheckCondition()
    {
        return true;
    }
}
