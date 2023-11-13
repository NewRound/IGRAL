using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textTitle;
    [SerializeField] private TextMeshProUGUI _textContent;

    [SerializeField] private Button _btnClose;
    [SerializeField] private Button _btnY;
    [SerializeField] private Button _btnN;

    private Action OnConfirmY;
    private Action OnConfirmN;

    private void Start()
    {
        _btnClose.onClick.AddListener(Close);
        _btnY.onClick.AddListener(ConfirmY);
        _btnN.onClick.AddListener(ConfirmN);
    }

    public void SetPopup(string title, string content, Action onConfirmY, Action onConfirmN = null)
    {
        _textTitle.text = title;
        _textContent.text = content;

        OnConfirmY = onConfirmY;
        OnConfirmN = onConfirmN;

        if (onConfirmY == null)
        {
            _btnN.gameObject.SetActive(false);
        }
        else
        {
            _btnN.gameObject.SetActive(true);
        }
    }

    private void ConfirmY()
    {
        if (OnConfirmY != null)
        {
            OnConfirmY();
            OnConfirmY = null;
        }

        Close();
    }

    private void ConfirmN()
    {
        if (OnConfirmN != null)
        {
            OnConfirmN();
            OnConfirmN = null;
        }

        Close();
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }
}
