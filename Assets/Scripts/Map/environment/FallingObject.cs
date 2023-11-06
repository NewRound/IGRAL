using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public Transform spawnPoint;
    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.Instance.StatHandler.Damaged(damage);
            other.gameObject.transform.position = spawnPoint.position;
        }
        else if(other.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
