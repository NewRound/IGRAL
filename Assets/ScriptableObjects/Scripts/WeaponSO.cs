using UnityEngine;

[CreateAssetMenu(fileName ="WeaponData",menuName ="SO/ItemData/Weapon")]
public class WeaponSO : ItemBaseSO
{
    [field: Header("# Abilities")]
    
    [field: SerializeField] public float baseDamage { get; private set; }
    [field: SerializeField] public int baseCount { get; private set; }
    [field: SerializeField] public GameObject projectile { get; private set; }
    [field: SerializeField] public bool isEquipped { get; private set; }
}
