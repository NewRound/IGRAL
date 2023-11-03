using UnityEngine;

public class Drone : MonoBehaviour
{
    [SerializeField] private float _attackDamage;
    [SerializeField] private bool _targetInRange;
    [SerializeField] private float _attackRange;
    [SerializeField] private GameObject _projectile;

    private void Update()
    {
        // 사거리에 적이 들어오면
        // 총알을 발사

        // 플레이어를 따라서 이동
    }   

    // 총알을 발사하는 메서드
    private void OnFire()
    {

    }        
}
