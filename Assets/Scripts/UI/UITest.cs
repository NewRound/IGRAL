using UnityEngine;

public class UITest : MonoBehaviour
{
    public Item test;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(UIManager.Instance);

        Invoke("TestAddItem", 1f);
    }

    void TestAddItem()
    {
        UIInventory.Instance.AddItem(test);
    }

}
