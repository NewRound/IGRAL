using UnityEngine;

public class ItemDropTest : MonoBehaviour
{
    private void Start()
    {
        Invoke("ItemDrop", 1f);
    }

    private void ItemDrop()
    {
        //����� ��Ƽ��Ʈ�� �����Ǿ� ����
        ItemManager.Instance.RandomDropItem(transform.position, ItemType.Artifact);
    }

}
