using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITutorial : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _text;

    public void OpenTutorial(Sprite Icon, string text)
    {
        gameObject.SetActive(true);
        _icon.sprite = Icon;
        _text.text = text;
    }

    public void CloseTutorial()
    {
        _icon.sprite = null;
        _text.text = null;

        gameObject.SetActive(false);
    }
}
