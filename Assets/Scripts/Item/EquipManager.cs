using UnityEngine;

public class EquipManager : CustomSingleton<EquipManager>
{
    [SerializeField] private GameObject backWeaponPos;
    [SerializeField] private GameObject HandWeaponPos;       

    public void SetBackWeapon()
    {
        backWeaponPos.SetActive(true);
        HandWeaponPos.SetActive(false);        
    }

    public void SetHandWeapon()
    {
        backWeaponPos.SetActive(false);
        HandWeaponPos.SetActive(true);
    }
}
