using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot
{
    public ItemSO item;
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

    private UIStatSlot[] _statsSlotList = new UIStatSlot[(int)StatType.RollingForce];

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
        foreach (StatType enumItem in Enum.GetValues(typeof(StatType)))
        {
            if (enumItem >= StatType.RollingForce)
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
        UpdateStats();
    }

    public void OpenInventory()
    {
        _uiInventory.SetActive(true);
        GameManager.Instance.StopGameTime();
    }

    public void CloseInventory()
    {
        _uiInventory.SetActive(false);
        GameManager.Instance.PlayGameTime();
    }

    #region 스텟 관련

    public void UpdateStats()
    {
        _palyerDate = GameManager.Instance.StatHandler.Data;
        foreach (StatType enumItem in Enum.GetValues(typeof(StatType)))
        {
            UpdateStat(enumItem);
        }
    }

    private void UpdateStat(StatType statsCategory)
    {
        switch (statsCategory)
        {
            case StatType.MaxHealth:
                _statsSlotList[(int)StatType.MaxHealth].UpdateValue(_palyerDate.Health);
                break;
            case StatType.HealthRegen:
                _statsSlotList[(int)StatType.HealthRegen].UpdateValue(_palyerDate.HealthRegen);
                break;
            case StatType.MaxKcal:
                _statsSlotList[(int)StatType.MaxKcal].UpdateValue(_palyerDate.MaxKcal);
                break;
            case StatType.KcalPerAttack:
                _statsSlotList[(int)StatType.KcalPerAttack].UpdateValue(_palyerDate.KcalPerAttack);
                break;
            case StatType.Defense:
                _statsSlotList[(int)StatType.Defense].UpdateValue(_palyerDate.Defense);
                break;
            case StatType.EvasionProbability:
                _statsSlotList[(int)StatType.EvasionProbability].UpdateValue(_palyerDate.EvasionProbability);
                break;
            case StatType.InvincibleTime:
                _statsSlotList[(int)StatType.InvincibleTime].UpdateValue(_palyerDate.InvincibleTime);
                break;
            case StatType.Attack:
                _statsSlotList[(int)StatType.Attack].UpdateValue(_palyerDate.Attack);
                break;
            case StatType.AttackDelay:
                _statsSlotList[(int)StatType.AttackDelay].UpdateValue(_palyerDate.AttackDelay);
                break;
            case StatType.AttackRange:
                _statsSlotList[(int)StatType.AttackRange].UpdateValue(_palyerDate.AttackRange);
                break;
            case StatType.CriticalProbability:
                _statsSlotList[(int)StatType.CriticalProbability].UpdateValue(_palyerDate.CriticalProbability);
                break;
            case StatType.CriticalMod:
                _statsSlotList[(int)StatType.CriticalMod].UpdateValue(_palyerDate.CriticalMod);
                break;
            case StatType.SpeedMin:
                _statsSlotList[(int)StatType.SpeedMin].UpdateValue(_palyerDate.SpeedMin);
                break;
            case StatType.SpeedMax:
                _statsSlotList[(int)StatType.SpeedMax].UpdateValue(_palyerDate.SpeedMax);
                break;
            case StatType.KnockbackPower:
                _statsSlotList[(int)StatType.KnockbackPower].UpdateValue(_palyerDate.KnockbackPower);
                break;
            case StatType.KnockbackTime:
                _statsSlotList[(int)StatType.KnockbackTime].UpdateValue(_palyerDate.KnockbackTime);
                break;
            case StatType.JumpingForce:
                _statsSlotList[(int)StatType.JumpingForce].UpdateValue(_palyerDate.JumpingForce);
                break;
            case StatType.JumpingCountMax:
                _statsSlotList[(int)StatType.JumpingCountMax].UpdateValue(_palyerDate.JumpingCountMax);
                break;
        }
    }

    #endregion 스텟 관련

    #region 아이템 정보 출력
    private string[] DisplayStat(StatChange itemData)
    {
        string[] re = new string[2];
        re[0] = GetDescription.EnumToString(itemData.statType);
        switch(itemData.statsChangeType)
        {
            case StatsChangeType.Add:
                re[1] = $"{itemData.value} 증가";
                break;
            case StatsChangeType.Subtract:
                re[1] = $"{itemData.value} 감소";
                break;
            case StatsChangeType.Multiple:
                re[1] = $"{itemData.value}% 증가";
                break;
            case StatsChangeType.Divide:
                re[1] = $"{itemData.value}% 감소";
                break;
            case StatsChangeType.Override:
                re[1] = $"{itemData.value}로 고정됨";
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

        StatChange[] itemDatas = selectedItem.item.ItemDatas;

        foreach (StatChange itemData in itemDatas)
        {
            string[] str = DisplayStat(itemData);

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

        StatChange[] itemDatas = selectedItem.item.ItemDatas;
        if (itemDatas != null)
        {
            foreach (StatChange itemData in itemDatas)
            {
                string[] str = DisplayStat(itemData);

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
    public void AddItem(ItemSO item)
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

    public void AddEquipItem(ItemSO item)
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
        List<StatChange> statChanges = new List<StatChange> ();

        for (int i = 0; i < _equipItems.Length; i++)
        {
            if (_equipItems[i].item != null)
            {
                _equipSlot[i].Set(_equipItems[i]);
                foreach(StatChange statChange in _equipItems[i].item.ItemDatas)
                {
                    statChanges.Add(statChange);
                }
            }
            else
                _equipSlot[i].Clear();
        }
        GameManager.Instance.StatHandler.UpdateStats(statChanges.ToArray());
        UpdateStats();
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
    }

    private void OnUnEquipButton()
    {
        AddItem(selectedItem.item);
        selectedItem.item = null;
        ClearInfo();
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
    private void ReturnItem(ItemSO item)
    {
        Debug.Log("장착칸이 가득 참");
        AddItem(item);
        //Instantiate(item.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360f));
    }

    private void ThrowItem(ItemSO item)
    {
        Debug.Log("인벤이 가득 참");
        //Instantiate(item.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360f));
    }
    #endregion 아이템 슬롯 부족

}
