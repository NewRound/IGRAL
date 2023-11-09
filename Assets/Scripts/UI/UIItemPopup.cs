using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemPopup : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _itemType;
    [SerializeField] private TextMeshProUGUI _itemName;

    public void OpenItemPopup(ItemSO item)
    {
        _icon.sprite = item.ItemIcon;
        switch (item.ItemType)
        {
            case ItemType.Artifact:
                _itemType.text = "<color=#F9E79F>[아티팩트]</color>";
                break;
            case ItemType.Consumable:
                _itemType.text = "<color=#85C1E9>[소비]</color>";
                break;
            case ItemType.Ingredient:
                _itemType.text = "<color=#C39BD3>[재료]</color>";
                break;
            case ItemType.Weapon:
                _itemType.text = "<color=#EC7063>[무기]</color>";
                break;

        }
        _itemName.text = item.ItemName;
    }

    public void CloseItemPopup()
    {
        _icon.sprite = null;
        _itemName.text = null;
    }
}
