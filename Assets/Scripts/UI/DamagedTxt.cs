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
    private Color _alpha;

    private void Start()
    {
        _txt = GetComponent<TextMeshPro>();
        _txt.text = _damage.ToString("F0");
        _alpha = _txt.color;
    }

    private void Init()
    {
        _timer = 0f;
        _alpha.a = 1f;
        _txt.color = _alpha;
    }

    void Update()
    {
        MoveUp();
        AlphaChange();

        _timer += Time.deltaTime;
        if (_timer > _duration )
        {
            Init();
        }
    }    

    private void MoveUp()
    {
        transform.Translate(new Vector3(0, _moveSpeed * Time.deltaTime, 0));
    }

    private void AlphaChange()
    {
        _alpha.a = Mathf.Lerp(_alpha.a, 0, _alphaSpeed * Time.deltaTime);
        _txt.color = _alpha;
    }    
}
