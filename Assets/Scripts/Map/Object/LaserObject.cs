using UnityEngine;

public class LaserObject : MonoBehaviour
{
    [SerializeField] private Transform SpawnPoint;
    [SerializeField] public float Damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerAppearanceController PAC;
        if (other.TryGetComponent<PlayerAppearanceController>(out PAC))
        {
            if (PAC.mutantType != MutantType.Skin)
            {
                // todo
                GameManager.Instance.StatHandler.Damaged(Damage);
                if (SpawnPoint != null)
                    other.transform.position = SpawnPoint.position;
            }
        }
    }
}
