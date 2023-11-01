using System.Collections.Generic;
using UnityEngine;

public class EquipManager : CustomSingleton<EquipManager>
{
    [SerializeField] private GameObject backWeaponPos;
    [SerializeField] private GameObject HandWeaponPos;

    private List<Item> curEquipments;

    public PlayerSO EquipmentStat()
    {
        // 인벤토리에서 현재 장착된 것들을 가져오기
        // curEquipments = ???

        // 능력치들을 다 더한 장비스탯 만들기
        PlayerSO equipmentSO = new PlayerSO();

       
        foreach (var item in curEquipments)
        {
            // curEquipments 들의 능력치를 합산하기
            // equipmentSO 에 합산한 능력치 할당?
        }

        return equipmentSO;
    }

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
