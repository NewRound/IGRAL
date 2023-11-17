using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyObject : MonoBehaviour, IInteract
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

    // 문제점
    // 새로운 객체가 생기면서 플레이어가 갇힘

    public void Interact()
    {
        if (_player.mutantType != MutantType.Stone) return;

        Debug.Log("공격받음");
        DestroyObj();
        CreateObj();
    }

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

    //private bool CheckMutantType()
    //{
    //    if (_player.mutantType == MutantType.Stone) return true;

    //    return false;
    //}   
}
