using System.Collections.Generic;
using UnityEngine;

public class EquipManager : CustomSingleton<EquipManager>
{
    [SerializeField] private GameObject backWeaponPos;
    [SerializeField] private GameObject HandWeaponPos;

    private List<Item> curEquipments;

    public PlayerSO EquipmentStat()
    {
        // �κ��丮���� ���� ������ �͵��� ��������
        // curEquipments = ???

        // �ɷ�ġ���� �� ���� ����� �����
        PlayerSO equipmentSO = new PlayerSO();

       
        foreach (var item in curEquipments)
        {
            // curEquipments ���� �ɷ�ġ�� �ջ��ϱ�
            // equipmentSO �� �ջ��� �ɷ�ġ �Ҵ�?
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
