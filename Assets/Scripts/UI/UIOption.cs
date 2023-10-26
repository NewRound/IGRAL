using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIOption : MonoBehaviour
{
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
    }

    private void OnEnable()
    {
        OpenPanel();

        //Time.timeScale = 0f;
    }

    private void OnDisable()
    {

        //Time.timeScale = 1.0f;
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
