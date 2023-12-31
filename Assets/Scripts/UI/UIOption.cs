using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIOption : MonoBehaviour
{

    [SerializeField] private Button _close;

    private int _currentidx;
    [SerializeField] private Button[] _optionBtns;
    [SerializeField] private TextMeshProUGUI[] _texts;
    [SerializeField] private GameObject[] _optionPanels;

    private void Awake()
    {
        for(int i = 0; i < _optionBtns.Length; i++)
        {
            int index = i;
            _optionBtns[i].onClick.AddListener(() => OpenPanel(index));
        }

        _close.onClick.AddListener(CloseOption);
    }

    private void CloseOption()
    {
        UIManager.Instance.CloseUI<UIOption>();
    }

    private void OnEnable()
    {
        OpenPanel();
        GameManager.Instance.StopGameTime();
    }

    private void OnDisable()
    {
        GameManager.Instance.PlayGameTime();
    }

    private void OpenPanel()
    {
        for (int i = 0; i < _optionPanels.Length; i++)
        {
            if (_currentidx == i)
            {
                _optionPanels[i].SetActive(true);
                _texts[i].color = Color.white;
            }
            else
            {
                _optionPanels[i].SetActive(false);
                _texts[i].color = Color.gray;
            }
        }
    }

    public void OpenPanel(int index)
    {
        _currentidx = index;
        OpenPanel();
    }


}
