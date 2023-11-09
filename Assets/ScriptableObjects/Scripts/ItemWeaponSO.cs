using UnityEngine;

[CreateAssetMenu(fileName = "ItemWeaponData", menuName = "SO/Item/ItemWeaponData")]
public class ItemWeaponSO : ItemSO
{
    [field: SerializeField] public GameObject WeaponObject { get; private set; }
}
