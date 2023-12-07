using UnityEngine;
using TMPro;

public class DamagedTxt : MonoBehaviour
{
    [SerializeField] private float _moveSpeed; // 텍스트 이동속도
    [SerializeField] private float _alphaSpeed; // 투명도 변환속도
    [SerializeField] private float _duration;

    private float _timer;
    private float _damage;

    private TextMeshPro _txt;
    private Color _textColor;

    private void Awake()
    {
        _txt = GetComponent<TextMeshPro>();
        _textColor = _txt.color;
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
        _txt.text = _damage.ToString("F0");
    }

    private void OnDisable()
    {
        _timer = 0f;
        _textColor.a = 1f;
        _damage = 0f;
    }

    void Update()
    {
        MoveUp();
        AlphaChange();

        _timer += Time.deltaTime;
        if (_timer > _duration )
        {
            gameObject.SetActive( false );
        }
    }    

    private void MoveUp()
    {
        transform.Translate(new Vector3(0, _moveSpeed * Time.deltaTime, 0));
    }

    private void AlphaChange()
    {
        _textColor.a = Mathf.Lerp(_textColor.a, 0, _alphaSpeed * Time.deltaTime);
        _txt.color = _textColor;
    }    
}
