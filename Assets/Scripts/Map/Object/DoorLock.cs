using System.Collections;
using UnityEngine;

public class DoorLock : MonoBehaviour, IObject
{
    [SerializeField] private Transform Destination;
    
    public bool IsLocked;

    private Vector3 StartPosition;
    private float elapsedTime = 0;                          // 이동이후 걸리는 시간(필요한가?)
    protected bool isMoving = false;                        // 움직이는 중인가?.


    private void Start()
    {
        StartPosition = transform.position;
    }

    public void Use()
    {
        if (!isMoving)
            StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        isMoving = true;

        Vector3 _startPosition = transform.position;
        Vector3 _destination;
        if (IsLocked)
            _destination = StartPosition;
        else
            _destination = Destination.position;

        while (elapsedTime < 1.1f)
        {
            transform.position = Vector3.Lerp(_startPosition, _destination, elapsedTime);
            elapsedTime += Time.deltaTime * 0.5f;
            yield return null;
        }

        elapsedTime = 0f;
        isMoving = false;

        IsLocked = !IsLocked;
    }
}
