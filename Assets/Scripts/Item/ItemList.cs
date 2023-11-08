using UnityEngine;

public class ItemList : MonoBehaviour
{
    [field: SerializeField] public Item[] itemArtifact { get; private set; }
    [field: SerializeField] public Item[] itemConsumable { get; private set; }
    [field: SerializeField] public Item[] itemIngredient { get; private set; }
    [field: SerializeField] public Item[] itemWeapons { get; private set; }

}
