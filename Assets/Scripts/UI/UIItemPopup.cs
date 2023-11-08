using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemPopup : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _itemName;

    public void OpenItemPopup(Item item)
    {
        _icon.sprite = item.ItemIcon;
        _itemName.text = item.ItemName;
    }

    public void CloseItemPopup()
    {
        _icon.sprite = null;
        _itemName.text = null;
    }
}
