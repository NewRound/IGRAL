using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : CustomSingleton<UIManager>
{
    private Dictionary<string, GameObject> _uiList = new Dictionary<string, GameObject>();

    public string[] uiType = { "UIMain", "UIController", "UIPlayerConditions", "UIBossCondition" };
    public string[] closeUi = { "UIPopUp", "UIItem"};

    private void Awake()
    {
        foreach (string type in uiType)
        {
            GameObject ui = Resources.Load<GameObject>($"UI/{type}");
            GameObject instantiate = Instantiate(ui, Vector3.zero, Quaternion.identity);
            instantiate.transform.SetParent(this.transform);
        }

        InitUIList();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void InitUIList()
    {
        int uiCount = transform.childCount;
        for (int i = 0; i < uiCount; i++)
        {
            var tr = transform.GetChild(i);
            _uiList.Add(uiType[i], tr.gameObject);
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
        foreach (string type in closeUi)
        {
            var obj = _uiList[type];
            obj.SetActive(false);
        }
    }
}
