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

    // ������
    // OnCollsionEnter > ����, ��ġ���·� ������ ���� �ʾƵ� ������ ���� �۵� 
    // ������ �� ���� �۵��ϰ� �ϴ� ���?

    // �ٴ��� �����Ǹ鼭 �÷��̾ ��������

    private void DestroyObj()
    {
        _audioSource.PlayOneShot(_audioClip); // ȿ����
        SpreadParticle(); // ����
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
        Debug.Log(" �ٴ� ���� ");
        Instantiate(_createObj, transform.position + new Vector3(-5f, -2f, 0f), Quaternion.identity);
    }

    private bool CheckMutantType()
    {
        if (_player.mutantType == MutantType.Stone) return true;

        return false;
    }
}
