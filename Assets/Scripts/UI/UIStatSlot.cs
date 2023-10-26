using TMPro;
using UnityEngine;

public class UIStatSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _category;
    [SerializeField] private TextMeshProUGUI _value;

    public void SetCategory(string category)
    {
        _category.text = category;
    }

    public void UpdateValue(float value)
    {
        _value.text = value.ToString();
    }
}
