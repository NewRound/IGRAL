using UnityEngine;

public class ItemList : MonoBehaviour
{
    [field: SerializeField] public ItemSO[] itemArtifact { get; private set; }
    [field: SerializeField] public ItemSO[] itemConsumable { get; private set; }
    [field: SerializeField] public ItemSO[] itemIngredient { get; private set; }
    [field: SerializeField] public ItemSO[] itemWeapons { get; private set; }

}
