using UnityEngine;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour
{
    public int index;

    [SerializeField] private Button _button;
    [SerializeField] private Image _icon;
    private ItemSlot curSlot;

    private void Awake()
    {
        _button.onClick.AddListener(OnClick);
    }

    public void Clear()
    {
        curSlot = null;
        _icon.gameObject.SetActive(false);
    }

    public void Set(ItemSlot slot)
    {
        curSlot = slot;
        _icon.gameObject.SetActive(true);
        _icon.sprite = slot.item.ItemIcon;
    }

    private void OnClick()
    {
        UIInventory.Instance.SelectItem(index);
    }
}
