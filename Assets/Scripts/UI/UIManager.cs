using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum UiType
{
    UITopButton
    , UIPlayerConditions
    , UIController
    , UIInventory

    , UIOption
    , UIBossCondition
    , UIPopup
}

public class UIManager : CustomSingleton<UIManager>
{
    private Dictionary<string, GameObject> _uiList = new Dictionary<string, GameObject>();

    private void Awake()
    {
        foreach (UiType enumItem in Enum.GetValues(typeof(UiType)))
        {
            GameObject ui = Resources.Load<GameObject>($"UI/{GetDescription.EnumToString(enumItem)}");
            GameObject instantiate = Instantiate(ui, Vector3.zero, Quaternion.identity);
            instantiate.transform.SetParent(this.transform);
        }

        InitUIList();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void InitUIList()
    {
        int i = 0;
        foreach (UiType enumItem in Enum.GetValues(typeof(UiType)))
        {
            var tr = transform.GetChild(i++);
            _uiList.Add(GetDescription.EnumToString(enumItem), tr.gameObject);
        }
    }

    private void Start()
    {
        int i = 0;
        foreach (UiType enumItem in Enum.GetValues(typeof(UiType)))
        {
            if ((int)enumItem > 3)
            { 
                var tr = transform.GetChild(i);
                tr.gameObject.SetActive(false);
            }
            i++;
        }
    }

    public T OpenUI<T>()
    {
        var obj = _uiList[typeof(T).Name];
        obj.SetActive(true);
        return obj.GetComponent<T>();
    }

    public T CloseUI<T>()
    {
        var obj = _uiList[typeof(T).Name];
        obj.SetActive(false);
        return obj.GetComponent<T>();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
    }
}
