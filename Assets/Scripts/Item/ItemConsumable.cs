using UnityEngine;

public class ItemConsumable : MonoBehaviour
{
    [field: SerializeField] public ItemSO item { get; private set; }

    public virtual void UseConsumable()
    {
        UIController.Instance.DelConsumableItem();

    }
}
