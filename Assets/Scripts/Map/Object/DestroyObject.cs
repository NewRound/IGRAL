using UnityEngine;

public class DestroyObject : MonoBehaviour, IInteract
{
    [SerializeField] private GameObject _destroyableObj;
    [SerializeField] private GameObject _destroyedObj;
    [SerializeField] private GameObject[] _particles;
    private float _spreadPower;

    private PlayerAppearanceController _player;
    private AudioManager _audioManager;
    private EffectManager _effectManager;

    private Collider _myCollider;

    private void Awake()
    {
        _myCollider = GetComponent<Collider>();
    }

    private void Start()
    {
        _myCollider.enabled = true;        
        _audioManager = AudioManager.Instance;
        _effectManager = EffectManager.Instance;

        _destroyableObj.SetActive(true);
        if (_destroyedObj != null) _destroyedObj.SetActive(false);

        for(int i = 0; i< _particles.Length; i++)
        {
            _particles[i].SetActive(false);
        }
    }

    public void Interact()
    {
        if (_player == null)
        {
            _player = GameManager.Instance.PlayerAppearanceController;
        }

        if (_player.mutantType != MutantType.Stone) return;

        _audioManager.PlaySFX(SFXType.ObjDestroy);
        _effectManager.ShowEffect(_particles[0].transform.position, EffectType.Explosion);

        SpreadParticle(); // ÆÄÆí

        _myCollider.enabled = false;

        _destroyableObj.SetActive(false);        
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
