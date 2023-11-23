using System;
using UnityEngine;

public class CuttableObject : MonoBehaviour, IInteract
{
    [SerializeField] private GameObject _cuttableObj;
    [SerializeField] private GameObject _cuttedObj;

    [SerializeField] private GameObject _movingObj;
    private ElevatorObject _elevator;

    private PlayerAppearanceController _player;
    private AudioManager _audioManager;

    private Collider _myCollider;

    public event Action cutAction;

    private void Awake()
    {
        _myCollider = GetComponent<Collider>();

    }

    private void Start()
    {
        _myCollider.enabled = true;

        _player = GameManager.Instance.PlayerAppearanceController;
        _audioManager = AudioManager.Instance;

        _cuttableObj.SetActive(true);
        if (_cuttedObj != null) _cuttedObj.SetActive(false);
        if (_movingObj != null) _elevator = _movingObj.GetComponent<ElevatorObject>();
        cutAction += _elevator.Use;
    }

    public void Interact()
    {
        if (_player.mutantType != MutantType.Blade) return;

        _audioManager.PlaySFX(SFXType.Swing);

        _myCollider.enabled = false;

        _cuttableObj.SetActive(false);
        if (_cuttedObj != null) _cuttedObj.SetActive(true);

        cutAction?.Invoke();
    }
}
