using UnityEngine;
using UnityEngine.UI;

public class UIEnemyHealth : MonoBehaviour
{
    [SerializeField] private Image uiHealth;
    [SerializeField] private Image uiArmor;

    private Quaternion _cameraRotation;
    private bool _isDisplay = false;
    private float _displayTime = 1f;
    private float _time = 0f;

    private void Start()
    {
        _cameraRotation = GameManager.Instance.Camera.transform.rotation;
    }

    private void Update()
    {
        if (!_isDisplay)
            return;

        transform.LookAt(transform.position + _cameraRotation * Vector3.forward, _cameraRotation * Vector3.up);

        _time += Time.deltaTime;
        if(_time >= _displayTime)
        {
            _isDisplay = false;
            gameObject.SetActive(false);
        }
    }

    public void DisplayEnemyHealth(float curValue, float maxValue)
    {
        if(maxValue > 0)
        {
            curValue = curValue < 0 ? 0 : curValue;

            gameObject.SetActive(true);

            _time = 0f;
            _isDisplay = true;
            uiHealth.fillAmount = (curValue / maxValue);
        }
    }

    public void DisplayEnemyArmor(float curValue, float maxValue)
    {
        if (maxValue > 0)
        {
            curValue = curValue < 0 ? 0 : curValue;

            gameObject.SetActive(true);

            _time = 0f;
            _isDisplay = true;
            uiArmor.fillAmount = (curValue / maxValue);
        }
    }

}
