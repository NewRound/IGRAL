using System.Collections;
using UnityEngine;

public class ElevatorObject : MonoBehaviour, IMovingObject
{
    private Vector3 StartPosition;

    [SerializeField] public Transform Destination;
    [SerializeField] public float moveSpeed;

    private float elapsedTime = 0;                          // �̵����� �ɸ��� �ð�(�ʿ��Ѱ�?)
    protected bool isMoving = false;                        // �����̴� ���ΰ�?.

    private void Awake()
    {
        StartPosition = transform.position;
    }

    public void Use()
    {
        if (!isMoving)
        {
            if (Vector3.Distance(transform.position, Destination.position) < 0.1)
            {
                StartCoroutine(Move(transform.position, StartPosition));
            }
            else if (Vector3.Distance(transform.position, StartPosition) < 0.1)
            {
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
        GameManager.Instance.PlayerInputController.transform.parent = transform;

        // �־��� �ð� ���� �ε巴�� �̵�
        while (elapsedTime < 1.1f)
        {
            transform.position = Vector3.Lerp(startingPos, targetPos, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed;
            Debug.Log("Move");
            yield return null;
        }

        // �̵� �Ϸ� �� �߰� �۾� ���� ����
        elapsedTime = 0f;
        isMoving = false;
        GameManager.Instance.PlayerInputController.transform.parent = null;
    }

    public bool CheckCondition()
    {
        return true;
    }
}
