using UnityEngine;
using UnityEngine.UI;

public class UITopButton : MonoBehaviour
{
    [SerializeField] private Button _option;
    [SerializeField] private Button _inventory;

    private void Awake()
    {
        _option.onClick.AddListener(OpenOption);
        _inventory.onClick.AddListener(OpenInventory);
    }

    private void OpenOption()
    {
        UIManager.Instance.OpenUI<UIOption>();
    }

    private void OpenInventory()
    {
        UIInventory.Instance.OpenInventory();
    }
}
