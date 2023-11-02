using TMPro;
using UnityEngine;

public class UISkillCategory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _skillCategoryName;
    [SerializeField] private GameObject _activate;
    public GameObject skillTree;

    private void Awake()
    {
        CategoryClose();
    }

    public void SetSkillCategory(string skillCategoryName)
    {
        _skillCategoryName.text = skillCategoryName;
    }

    public virtual void CategoryOpen()
    {
        _activate.SetActive(true);
        skillTree.SetActive(true);
    }

    public void CategoryClose()
    {
        _activate.SetActive(false);
        skillTree.SetActive(false);
    }
}
