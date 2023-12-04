using System.Collections.Generic;
using UnityEngine;

public class ColTrigger : MonoBehaviour
{
    [SerializeField] List<GameObject> gameObjects;
    private bool _isActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!_isActive && other.TryGetComponent<InputController>(out InputController player))
        {
            foreach (GameObject _gameObject in gameObjects)
            {
                if (_gameObject.TryGetComponent<IObject>(out IObject useGameobject))
                {
                    useGameobject.Use();
                }
            }
            _isActive = true;
        }
    }
}
