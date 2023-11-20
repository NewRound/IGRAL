using UnityEngine;

public class DestroyObject : MonoBehaviour, IInteract
{
    [SerializeField] private GameObject _destroyableObj;
    [SerializeField] private GameObject _destroyedObj;
    [SerializeField] private GameObject[] _particles;
    [SerializeField] private LayerMask _canBeDestroyBy;

    private PlayerAppearanceController _player;

    private AudioSource _audioSource;
    private AudioClip _audioClip;
    
    private void Start()
    {
        _player = GameManager.Instance.PlayerAppearanceController;
        _audioSource = GetComponent<AudioSource>();
        _audioClip = _audioSource.clip;
    }

    public void Interact()
    {
        if (_player.mutantType != MutantType.Stone) return;

        Debug.Log("공격받음");

        _audioSource.PlayOneShot(_audioClip); // 효과음
        SpreadParticle(); // 파편

        _destroyableObj.SetActive(false);
        if (_destroyedObj != null) _destroyedObj.SetActive(true);
    }

    private void SpreadParticle()
    {
        for (int i = 0; i < _particles.Length; i++)
        {
            float x = Random.Range(-2f, 2f);
            float y = Random.Range(0f, 2f);
            float z = Random.Range(-0.5f, 0.5f);

            Vector3 randPos = new Vector3(x, y, z);

            _particles[i].SetActive(true);
            Rigidbody particleRigid = _particles[i].GetComponent<Rigidbody>();

            if (particleRigid != null)
            {
                particleRigid.AddForce(randPos,ForceMode.Impulse);
                Destroy(_particles[i], 5f);
            }
        }
    }
}
