using UnityEngine;

public class ItemImageTest : MonoBehaviour
{
    public ItemSO test;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(UIManager.Instance);

        Invoke("TestAddItem", 1f);
    }

    void TestAddItem()
    {
        UIInventory.Instance.AddItem(test);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
