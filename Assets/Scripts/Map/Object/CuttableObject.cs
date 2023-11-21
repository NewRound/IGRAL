using System;
using UnityEngine;

public class CuttableObject : MonoBehaviour, IInteract
{
    [SerializeField] private GameObject _cuttableObj;
    [SerializeField] private GameObject _cuttedObj;

    [SerializeField] private GameObject _movingObj;
    private ElevatorObject _elevator;

    private PlayerAppearanceController _player;

    private Collider _myCollider;
    private AudioSource _audioSource;
    private AudioClip _audioClip;

    public event Action cutAction;

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

        _cuttableObj.SetActive(true);
        if (_cuttedObj != null) _cuttedObj.SetActive(false);
        if (_movingObj != null) _elevator = _movingObj.GetComponent<ElevatorObject>();
        cutAction += _elevator.Use;
    }

    public void Interact()
    {
        if (_player.mutantType != MutantType.Blade) return;

        _audioSource.PlayOneShot(_audioClip); // È¿°úÀ½
                
        _myCollider.enabled = false;

        _cuttableObj.SetActive(false);
        if (_cuttedObj != null) _cuttedObj.SetActive(true);

        cutAction?.Invoke();
    }
}
