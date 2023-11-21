using UnityEngine;

public class DestroyObject : MonoBehaviour, IInteract
{
    [SerializeField] private GameObject _destroyableObj;
    [SerializeField] private GameObject _destroyedObj;
    [SerializeField] private GameObject[] _particles;
    private float _spreadPower;

    private PlayerAppearanceController _player;

    private Collider _myCollider;
    private AudioSource _audioSource;
    private AudioClip _audioClip;

    private void Awake()
    {
        _myCollider = GetComponent<Collider>();

        _audioSource = GetComponent<AudioSource>();
        _audioClip = _audioSource.clip;
    }

    private void Start()
    {
        _myCollider.enabled = true;

        _player = GameManager.Instance.PlayerAppearanceController;

        _destroyableObj.SetActive(true);
        _destroyedObj.SetActive(false);

        for(int i = 0; i< _particles.Length; i++)
        {
            _particles[i].SetActive(false);
        }
    }

    public void Interact()
    {
        Debug.Log("상호작용");
        if (_player.mutantType != MutantType.Stone) return;

        _audioSource.PlayOneShot(_audioClip); // 효과음
        SpreadParticle(); // 파편

        _destroyableObj.SetActive(false);
        _myCollider.enabled = false;
        if (_destroyedObj != null) _destroyedObj.SetActive(true);
    }

    private void SpreadParticle()
    {
        for (int i = 0; i < _particles.Length; i++)
        {
            float x = Random.Range(-0.5f, 0.5f);
            float y = Random.Range(0.5f, 0.5f);
            float z = Random.Range(-0.5f, 0.5f);

            Vector3 randPos = new Vector3(x, y, z);
            _spreadPower = Random.Range(1f, 3f);

            _particles[i].SetActive(true);
            Rigidbody particleRigid = _particles[i].GetComponent<Rigidbody>();

            if (particleRigid != null)
            {
                particleRigid.AddForce(randPos * _spreadPower, ForceMode.Impulse);
                Destroy(_particles[i], 3f);
            }
        }
    }
}
