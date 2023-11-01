using UnityEngine;

public class EquipManager : CustomSingleton<EquipManager>
{
    [SerializeField] private GameObject backEquip;
    [SerializeField] private GameObject HandEquip;       

    public void SetBackWeapon()
    {
        backEquip.SetActive(true);
        HandEquip.SetActive(false);        
    }

    public void SetHandWeapon()
    {
        backEquip.SetActive(false);
        HandEquip.SetActive(true);
    }
}
