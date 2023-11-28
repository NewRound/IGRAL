using UnityEngine;
using UnityEngine.UI;

public class UIEnemyHealth : MonoBehaviour
{
    [SerializeField] private Image uiHealth;
    [SerializeField] private Image uiArmor;

    private Quaternion _cameraRotation;

    private void Start()
    {
        _cameraRotation = GameManager.Instance.Camera.transform.rotation;
    }

    private void Update()
    {
        transform.LookAt(transform.position + _cameraRotation * Vector3.forward, _cameraRotation * Vector3.up);
    }

    public void DisplayEnemyHealth(float curValue, float maxValue)
    {
        if(maxValue > 0)
        {
            curValue = curValue < 0 ? 0 : curValue;

            gameObject.SetActive(true);
            uiHealth.fillAmount = (curValue / maxValue);
        }
    }

    public void DisplayEnemyArmor(float curValue, float maxValue)
    {
        if (maxValue > 0)
        {
            curValue = curValue < 0 ? 0 : curValue;

            gameObject.SetActive(true);
            uiArmor.fillAmount = (curValue / maxValue);
        }
    }

}
