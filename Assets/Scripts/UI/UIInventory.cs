using System;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum StatsCategory
{
    [Description("체력")] Health 
    , [Description("체력 회복 속도")] HealthRegen 
    , [Description("방어력")] Defense
    , [Description("회피 확률")] EvasionProbability 
    , [Description("공격력")] Attack 
    , [Description("공격딜레이")] AttackDelay
    , [Description("공격 범위")] AttackRange
    , [Description("크리티컬 확률")] CriticalProbability 
    , [Description("크리티컬 데미지 배율")] CriticalMod 
    , [Description("이동속도")] SpeedMin 
    , [Description("넉백파워")] KnockbackPower 
    , [Description("넉백시간")] KnockbackTime 
    , [Description("점프력")] JumpForce 
    , [Description("점프 횟수")] MaxJumpCount 
    , [Description("칼로리 회복")] KcalPerAttack 
    , [Description("칼로리")] MaxKcal 


    , Max
}

public class ItemSlot
{
    public IItem item;
}


public class UIInventory : CustomSingleton<UIInventory>
{
    private PlayerSO _palyerDate;

    [Header("main")]
    [SerializeField] private GameObject _uiInventory;
    [SerializeField] private Button _close;

    [Header("stats")]
    [SerializeField] private Transform _statsContent;
    [SerializeField] private GameObject _statsSlot;

    [Header("itemInfo")]
    [SerializeField] private TextMeshProUGUI _selectedItemName;
    [SerializeField] private TextMeshProUGUI _selectedItemDescription;
    [SerializeField] private TextMeshProUGUI _selectedItemStatNames;
    [SerializeField] private TextMeshProUGUI _selectedItemStatValues;
    [SerializeField] private TextMeshProUGUI _setText;
    [SerializeField] private Button _equipButton;
    [SerializeField] private Button _unEquipButton;
    [SerializeField] private Button _dropEquipButton;

    [Header("equipItem")]
    [SerializeField] private Transform _equip;
    [SerializeField] private GameObject _uIEquipSlot;
    [SerializeField] private UIEquipSlot[] _equipSlot;

    [Header("inventoryItem")]
    [SerializeField] private Transform _inventory;
    [SerializeField] private GameObject _uIinventorySlot;
    [SerializeField] private UIInventorySlot[] _inventorySlot;

    private ItemSlot[] _equipItems;
    private ItemSlot[] _inventoryItems;

    private ItemSlot selectedItem;
    private int equipIndex;
    private int itemIndex;

    private UIStatSlot[] _statsSlotList = new UIStatSlot[(int)StatsCategory.Max];

    private void Awake()
    {
        _close.onClick.AddListener(CloseInventory);
        _equipSlot = new UIEquipSlot[6];
        for (int n = 0; n < 6; n++)
        {
            GameObject instantiate = Instantiate(_uIEquipSlot, _equip);
            UIEquipSlot uIEquipSlot = instantiate.GetComponent<UIEquipSlot>();
            _equipSlot[n] = uIEquipSlot;
        }

        _inventorySlot = new UIInventorySlot[12];
        for (int n = 0; n < 12; n++)
        {
            GameObject instantiate = Instantiate(_uIinventorySlot, _inventory);
            UIInventorySlot uIInventorySlot = instantiate.GetComponent<UIInventorySlot>();
            _inventorySlot[n] = uIInventorySlot;
        }

        int i = 0;
        foreach (StatsCategory enumItem in Enum.GetValues(typeof(StatsCategory)))
        {
            if (enumItem == StatsCategory.Max)
                return;

            GameObject instantiate = Instantiate(_statsSlot, _statsContent);
            UIStatSlot uIStatSlot = instantiate.GetComponent<UIStatSlot>();
            uIStatSlot.SetCategory(GetDescription.EnumToString(enumItem));
            _statsSlotList[i] = uIStatSlot;
            i++;
        }
    }

    private void Start()
    {
        _selectedItemName.text = "";
        _selectedItemDescription.text = "";
        _selectedItemStatNames.text = "";
        _selectedItemStatValues.text = "";

        _equipItems = new ItemSlot[_equipSlot.Length];

        for (int i = 0; i < _equipItems.Length; i++)
        {
            _equipItems[i] = new ItemSlot();
            _equipSlot[i].index = i;
            _equipSlot[i].Clear();
        }

        _inventoryItems = new ItemSlot[_inventorySlot.Length];
        for (int i = 0; i < _inventorySlot.Length; i++)
        {
            _inventoryItems[i] = new ItemSlot();
            _inventorySlot[i].index = i;
            _inventorySlot[i].Clear();
        }

        _equipButton.onClick.AddListener(OnEquipButton);
        _unEquipButton.onClick.AddListener(OnUnEquipButton);
        _dropEquipButton.onClick.AddListener(OnDropEquipButton);

        _equipButton.gameObject.SetActive(false);
        _unEquipButton.gameObject.SetActive(false);
        _dropEquipButton.gameObject.SetActive(false);

        _uiInventory.SetActive(false);

        _palyerDate = GameManager.Instance.StatHandler.Data;
        UpdateStats();
    }

    public void OpenInventory()
    {
        _uiInventory.SetActive(true);
        //Time.timeScale = 0f;
    }

    public void CloseInventory()
    {
        _uiInventory.SetActive(false);
        //Time.timeScale = 1f;
    }

    #region 스텟 관련

    public void UpdateStats()
    {
        foreach (StatsCategory enumItem in Enum.GetValues(typeof(StatsCategory)))
        {
            UpdateStat(enumItem);
        }
    }

    private void UpdateStat(StatsCategory statsCategory)
    {
        switch (statsCategory)
        {
            case StatsCategory.Health:
                _statsSlotList[(int)StatsCategory.Health].UpdateValue(_palyerDate.Health);
                break;
            case StatsCategory.HealthRegen:
                _statsSlotList[(int)StatsCategory.HealthRegen].UpdateValue(_palyerDate.HealthRegen);
                break;
            case StatsCategory.Defense:
                _statsSlotList[(int)StatsCategory.Defense].UpdateValue(_palyerDate.Defense);
                break;
            case StatsCategory.EvasionProbability:
                _statsSlotList[(int)StatsCategory.EvasionProbability].UpdateValue(_palyerDate.EvasionProbability);
                break;
            case StatsCategory.Attack:
                _statsSlotList[(int)StatsCategory.Attack].UpdateValue(_palyerDate.Attack);
                break;
            case StatsCategory.AttackDelay:
                _statsSlotList[(int)StatsCategory.AttackDelay].UpdateValue(_palyerDate.AttackDelay);
                break;
            case StatsCategory.AttackRange:
                _statsSlotList[(int)StatsCategory.AttackRange].UpdateValue(_palyerDate.AttackRange);
                break;
            case StatsCategory.CriticalProbability:
                _statsSlotList[(int)StatsCategory.CriticalProbability].UpdateValue(_palyerDate.CriticalProbability);
                break;
            case StatsCategory.CriticalMod:
                _statsSlotList[(int)StatsCategory.CriticalMod].UpdateValue(_palyerDate.CriticalMod);
                break;
            case StatsCategory.SpeedMin:
                _statsSlotList[(int)StatsCategory.SpeedMin].UpdateValue(_palyerDate.SpeedMin);
                break;
            case StatsCategory.KnockbackPower:
                _statsSlotList[(int)StatsCategory.KnockbackPower].UpdateValue(_palyerDate.KnockbackPower);
                break;
            case StatsCategory.KnockbackTime:
                _statsSlotList[(int)StatsCategory.KnockbackTime].UpdateValue(_palyerDate.KnockbackTime);
                break;
            case StatsCategory.JumpForce:
                _statsSlotList[(int)StatsCategory.JumpForce].UpdateValue(_palyerDate.JumpingForce);
                break;
            case StatsCategory.MaxJumpCount:
                _statsSlotList[(int)StatsCategory.MaxJumpCount].UpdateValue(_palyerDate.JumpingCountMax);
                break;
            case StatsCategory.KcalPerAttack:
                _statsSlotList[(int)StatsCategory.KcalPerAttack].UpdateValue(_palyerDate.KcalPerAttack);
                break;
            case StatsCategory.MaxKcal:
                _statsSlotList[(int)StatsCategory.MaxKcal].UpdateValue(_palyerDate.MaxKcal);
                break;
        }
    }

    #endregion 스텟 관련

    #region 아이템 정보 출력
    private string[] GetStat(PlayerSO itemData, StatsCategory statsCategory)
    {
        string[] re = null;
        switch (statsCategory)
        {
            case StatsCategory.Health:
                if (itemData.Health == 0)
                    return null;

                re = new string[2];
                re[0] = GetDescription.EnumToString(statsCategory);
                re[1] = itemData.Health.ToString();
                break;
            case StatsCategory.HealthRegen:
                if (itemData.HealthRegen == 0)
                    return null;

                re = new string[2];
                re[0] = GetDescription.EnumToString(statsCategory);
                re[1] = itemData.HealthRegen.ToString();
                break;
            case StatsCategory.Defense:
                if (itemData.Defense == 0)
                    return null;

                re = new string[2];
                re[0] = GetDescription.EnumToString(statsCategory);
                re[1] = itemData.Defense.ToString();
                break;
            case StatsCategory.EvasionProbability:
                if (itemData.EvasionProbability == 0)
                    return null;

                re = new string[2];
                re[0] = GetDescription.EnumToString(statsCategory);
                re[1] = itemData.EvasionProbability.ToString();
                break;
            case StatsCategory.Attack:
                if (itemData.Attack == 0)
                    return null;

                re = new string[2];
                re[0] = GetDescription.EnumToString(statsCategory);
                re[1] = itemData.Attack.ToString();
                break;
            case StatsCategory.AttackDelay:
                if (itemData.AttackDelay == 0)
                    return null;

                re = new string[2];
                re[0] = GetDescription.EnumToString(statsCategory);
                re[1] = itemData.AttackDelay.ToString();
                break;
            case StatsCategory.AttackRange:
                if (itemData.AttackRange == 0)
                    return null;

                re = new string[2];
                re[0] = GetDescription.EnumToString(statsCategory);
                re[1] = itemData.AttackRange.ToString();
                break;
            case StatsCategory.CriticalProbability:
                if (itemData.CriticalProbability == 0)
                    return null;

                re = new string[2];
                re[0] = GetDescription.EnumToString(statsCategory);
                re[1] = itemData.CriticalProbability.ToString();
                break;
            case StatsCategory.CriticalMod:
                if (itemData.CriticalMod == 0)
                    return null;

                re = new string[2];
                re[0] = GetDescription.EnumToString(statsCategory);
                re[1] = itemData.CriticalMod.ToString();
                break;
            case StatsCategory.SpeedMin:
                if (itemData.SpeedMin == 0)
                    return null;

                re = new string[2];
                re[0] = GetDescription.EnumToString(statsCategory);
                re[1] = itemData.SpeedMin.ToString();
                break;
            case StatsCategory.KnockbackPower:
                if (itemData.KnockbackPower == 0)
                    return null;

                re = new string[2];
                re[0] = GetDescription.EnumToString(statsCategory);
                re[1] = itemData.KnockbackPower.ToString();
                break;
            case StatsCategory.KnockbackTime:
                if (itemData.KnockbackTime == 0)
                    return null;

                re = new string[2];
                re[0] = GetDescription.EnumToString(statsCategory);
                re[1] = itemData.KnockbackTime.ToString();
                break;
            case StatsCategory.JumpForce:
                if (itemData.JumpingForce == 0)
                    return null;

                re = new string[2];
                re[0] = GetDescription.EnumToString(statsCategory);
                re[1] = itemData.JumpingForce.ToString();
                break;
            case StatsCategory.MaxJumpCount:
                if (itemData.JumpingCountMax == 0)
                    return null;

                re = new string[2];
                re[0] = GetDescription.EnumToString(statsCategory);
                re[1] = itemData.JumpingCountMax.ToString();
                break;
            case StatsCategory.KcalPerAttack:
                if (itemData.KcalPerAttack == 0)
                    return null;

                re = new string[2];
                re[0] = GetDescription.EnumToString(statsCategory);
                re[1] = itemData.KcalPerAttack.ToString();
                break;
            case StatsCategory.MaxKcal:
                if (itemData.MaxKcal == 0)
                    return null;

                re = new string[2];
                re[0] = GetDescription.EnumToString(statsCategory);
                re[1] = itemData.MaxKcal.ToString();
                break;
        }
        return re;
    }
    #endregion 아이템 정보 출력

    #region 아이템 슬롯 클릭
    public void SelectEquipItem(int index)
    {
        if (_equipItems[index].item == null)
        {
            ClearInfo();
            return;
        }
        ClearButton();

        selectedItem = _equipItems[index];
        equipIndex = index;

        _selectedItemName.text = selectedItem.item.ItemName;
        _selectedItemDescription.text = selectedItem.item.ItemInfo;

        _selectedItemStatNames.text = string.Empty;
        _selectedItemStatValues.text = string.Empty;

        PlayerSO itemData = selectedItem.item.ItemData;

        foreach (StatsCategory enumItem in Enum.GetValues(typeof(StatsCategory)))
        {
            string[] str = GetStat(itemData, enumItem);

            if(str != null && str.Length == 2)
            {
                _selectedItemStatNames.text += str[0] + "\n";
                _selectedItemStatValues.text += str[1] + "\n";
            }
        }
        _unEquipButton.gameObject.SetActive(true);
    }

    public void SelectItem(int index)
    {
        if (_inventoryItems[index].item == null)
        {
            ClearInfo();
            return;
        }
        ClearButton();

        selectedItem = _inventoryItems[index];
        itemIndex = index;

        _selectedItemName.text = selectedItem.item.ItemName;
        _selectedItemDescription.text = selectedItem.item.ItemInfo;

        _selectedItemStatNames.text = string.Empty;
        _selectedItemStatValues.text = string.Empty;

        PlayerSO itemData = selectedItem.item.ItemData;
        if(itemData != null)
        {
            foreach (StatsCategory enumItem in Enum.GetValues(typeof(StatsCategory)))
            {
                string[] str = GetStat(itemData, enumItem);

                if (str != null && str.Length == 2)
                {
                    _selectedItemStatNames.text += str[0] + "\n";
                    _selectedItemStatValues.text += str[1] + "\n";
                }
            }
        }
        _equipButton.gameObject.SetActive(true);
        _dropEquipButton.gameObject.SetActive(true);
    }
    #endregion 아이템 슬롯 클릭

    #region 아이템 슬롯 업데이트
    public void AddItem(IItem item)
    {


        ItemSlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.item = item;
            UpdateUI();
            return;
        }



        ThrowItem(item);
    }

    public void AddEquipItem(IItem item)
    {
        ItemSlot emptySlot = GetEquipEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.item = item;
            UpdateEquipUI();
            return;
        }

        ReturnItem(item);
    }


    private void UpdateUI()
    {
        for (int i = 0; i < _inventoryItems.Length; i++)
        {
            if (_inventoryItems[i].item != null)
                _inventorySlot[i].Set(_inventoryItems[i]);
            else
                _inventorySlot[i].Clear();
        }
    }

    private void UpdateEquipUI()
    {
        for (int i = 0; i < _equipItems.Length; i++)
        {
            if (_equipItems[i].item != null)
                _equipSlot[i].Set(_equipItems[i]);
            else
                _equipSlot[i].Clear();
        }
    }

    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < _inventoryItems.Length; i++)
        {
            if (_inventoryItems[i].item == null)
                return _inventoryItems[i];
        }
        return null;
    }

    ItemSlot GetEquipEmptySlot()
    {
        for (int i = 0; i < _equipItems.Length; i++)
        {
            if (_equipItems[i].item == null)
                return _equipItems[i];
        }
        return null;
    }

    private void ClearInfo()
    {
        selectedItem = null;
        itemIndex = -1;
        equipIndex = -1;

        _selectedItemName.text = string.Empty;
        _selectedItemDescription.text = string.Empty;
        _selectedItemStatNames.text = string.Empty;
        _selectedItemStatValues.text = string.Empty;

        ClearButton();
    }

    private void ClearButton()
    {
        _equipButton.gameObject.SetActive(false);
        _unEquipButton.gameObject.SetActive(false);
        _dropEquipButton.gameObject.SetActive(false);
    }

    #endregion 아이템 슬롯 업데이트

    #region 버튼 
    private void OnEquipButton()
    {
        AddEquipItem(selectedItem.item);
        selectedItem.item = null;
        ClearInfo();
        UpdateUI();
        UpdateEquipUI();
    }

    private void OnUnEquipButton()
    {
        AddItem(selectedItem.item);
        selectedItem.item = null;
        ClearInfo();
        UpdateUI();
        UpdateEquipUI();
    }

    private void OnDropEquipButton()
    {
        ThrowItem(selectedItem.item);
        RemoveSelectedItem();

    }

    private void RemoveSelectedItem()
    {
        selectedItem.item = null;

        ClearInfo();
        UpdateUI();
    }
    #endregion 버튼

    #region 아이템 슬롯 부족
    private void ReturnItem(IItem item)
    {
        Debug.Log("장착칸이 가득 참");
        AddItem(item);
        //Instantiate(item.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360f));
    }

    private void ThrowItem(IItem item)
    {
        Debug.Log("인벤이 가득 참");
        //Instantiate(item.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360f));
    }
    #endregion 아이템 슬롯 부족

}
