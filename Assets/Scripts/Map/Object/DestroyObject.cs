using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] private GameObject _createObj;
    [SerializeField] private LayerMask _canBeDestroyBy;
    private PlayerAppearanceController _player;

    private AudioSource _audioSource;
    private AudioClip _audioClip;

    [SerializeField] private List<GameObject> _particles;
    private List<Rigidbody> _rigids;

    private void Start()
    {
        _player = GameManager.Instance.PlayerAppearanceController;
        _audioSource = GetComponent<AudioSource>();
        _audioClip = _audioSource.clip;
        _rigids = new List<Rigidbody>();
    }    

    private void OnCollisionEnter(Collision collision)
    {
        if (!(_canBeDestroyBy.value == ( _canBeDestroyBy.value | (1 << collision.gameObject.layer)))) return;
        
        if (CheckMutantType())
        {
            DestroyObj();            
            CreateObj();
        }        
    }   

    // 문제점
    // OnCollsionEnter > 오류, 망치상태로 공격을 하지 않아도 가까이 가면 작동 
    // 공격을 할 때만 작동하게 하는 방법?

    // 바닥이 생성되면서 플레이어가 갇혀버림

    private void DestroyObj()
    {
        _audioSource.PlayOneShot(_audioClip); // 효과음
        SpreadParticle(); // 파편
        Destroy(gameObject, 1f);
    }

    private void SpreadParticle()
    {
        float x = Random.Range(-10f, 10f);
        float y = Random.Range(-10f, 10f);
        float z = Random.Range(-10f, 10f);

        Vector3 ranPos = new Vector3(x, y, z);

        foreach (GameObject particle in _particles)
        {
            GameObject instantiatedParticle = Instantiate(particle, transform.position, Quaternion.identity);
            Rigidbody particleRigidbody = instantiatedParticle.GetComponent<Rigidbody>();

            if (particleRigidbody != null)
            {
                _rigids.Add(particleRigidbody);
                particleRigidbody.AddForce(ranPos);
                Destroy(particleRigidbody.gameObject, 5f);
            }
        }
    }

    private void CreateObj()
    {
        Debug.Log(" 바닥 생성 ");
        Instantiate(_createObj, transform.position + new Vector3(-5f, -2f, 0f), Quaternion.identity);
    }

    private bool CheckMutantType()
    {
        if (_player.mutantType == MutantType.Stone) return true;

        return false;
    }
}
