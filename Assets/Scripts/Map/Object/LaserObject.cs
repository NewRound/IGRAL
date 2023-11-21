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
        GameObject otherObject = other.gameObject;

        PlayerAppearanceController PAC;
        if (other.TryGetComponent<PlayerAppearanceController>(out PAC))
        {
            if (PAC.mutantType != MutantType.Skin)
            {
                // 데미지 주기.
                // todo

                otherObject.transform.position = SpawnPoint.position;
            }
        }
    }
}
