using UnityEngine;
using UnityEngine.UI;

public class UITopButton : MonoBehaviour
{
    [SerializeField] private Button _option;
    [SerializeField] private Button _inventory;
    [SerializeField] private Button _skillTree;

    private void Awake()
    {
        _option.onClick.AddListener(OpenOption);
        _inventory.onClick.AddListener(OpenInventory);
        _skillTree.onClick.AddListener(OpenSkillTree);
    }

    private void OpenOption()
    {
        UIManager.Instance.OpenUI<UIOption>();
    }

    private void OpenInventory()
    {
        UIInventory.Instance.OpenInventory();
    }

    private void OpenSkillTree()
    {
        UISkillTree.Instance.OpenSkillTree();
    }
}
