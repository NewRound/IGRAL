using UnityEngine;

public class EffectTimer : MonoBehaviour
{
    [SerializeField] private float _maxDuration;
    private float _curDuration;

    private void OnEnable()
    {
        _curDuration = 0f;
    }
    
    private void Update()
    {
        _curDuration += Time.deltaTime;

        if( _curDuration >= _maxDuration )
        {
            gameObject.SetActive(false);
        }
    }
}
