using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum UIType
{
    UITopButton
    , UIPlayerConditions
    , UIController
    , UIInventory
    , UISkillTree

    , UIOption
    , UIBossCondition
    , UIPopup
    , UIItemPopup
    , UIGameOver




    , UITutorial
}

public class UIManager : CustomSingleton<UIManager>
{
    private Dictionary<string, GameObject> _uiList = new Dictionary<string, GameObject>();

    private void Awake()
    {
        foreach (UIType enumItem in Enum.GetValues(typeof(UIType)))
        {
            GameObject ui = Resources.Load<GameObject>($"UI/{enumItem}");
            GameObject instantiate = Instantiate(ui, Vector3.zero, Quaternion.identity);
            instantiate.transform.SetParent(this.transform);
        }

        InitUIList();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void InitUIList()
    {
        int i = 0;
        foreach (UIType enumItem in Enum.GetValues(typeof(UIType)))
        {
            var tr = transform.GetChild(i++);
            _uiList.Add(GetDescription.EnumToString(enumItem), tr.gameObject);
        }
    }

    private void Start()
    {
        InitOpenUI();


        if (GameManager.Instance.isTutorial)
            return;

        TutorialPopup();
    }

    public void InitOpenUI()
    {
        int i = 0;
        foreach (UIType enumItem in Enum.GetValues(typeof(UIType)))
        {
            if ((int)enumItem > 4)
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


    private void TutorialPopup()
    {
        UIPopup UIPopup = OpenUI<UIPopup>();
        UIPopup.SetPopup("튜토리얼", "튜토리얼을 진행 하시겠습니까?", () => { LoadSceneManager.Instance.LoadScene("TutorialMap"); }, () => { PlayerPrefs.SetInt("Tutorial", 1); GameManager.Instance.isTutorial = true; });
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitOpenUI();
    }
}
