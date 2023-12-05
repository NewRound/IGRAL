using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerSOData
{
    public bool isNotNew;
    public int skillPoint;
    public string playerSO;
    public Dictionary<string, int> learnedSkills;

    public void SetPlayerSO(PlayerSO SO)
    {
        playerSO =  JsonUtility.ToJson(SO);
    }
}

public class DataManager : CustomSingleton<DataManager>
{
    private PlayerSO _playerSO;
    public PlayerSO playerSO { get; private set; }

    private void Awake()
    {
        //PlayerPrefs.SetInt("Tutorial", 0);
        _playerSO = Resources.Load<PlayerSO>($"Data/PlayerData");
        playerSO = Instantiate(_playerSO);
        Debug.Log(playerSO.Attack);
    }

    public void BackUpPlayerSO()
    {
        playerSO = Instantiate(GameManager.Instance.StatHandler.Data);
        SavePlayerSO();
    }

    public void LoadPlayerSO()
    {
        string jsonString = System.IO.File.ReadAllText(Application.persistentDataPath + @"\playerSOData.json");
        Debug.Log(jsonString);
        if (jsonString != null)
        {
            PlayerSOData playerSOData = JsonUtility.FromJson<PlayerSOData>(jsonString);
            if (!playerSOData.isNotNew)
                return;
            Debug.Log(playerSO);
            if (playerSOData.playerSO == null && playerSOData.playerSO == "")
                return;
            playerSO = JsonUtility.FromJson<PlayerSO>(playerSOData.playerSO);

            if (playerSOData.learnedSkills == null)
                return;
            SkillManager.Instance.learnedSkills = playerSOData.learnedSkills;
            SkillManager.Instance.skillPoint = playerSOData.skillPoint;
        }
    }

    public void DeletePlayerSO()
    {
        PlayerSOData data = new PlayerSOData();
        data.isNotNew = false;
        string json = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(Application.persistentDataPath + @"\playerSOData.json", json);
    }

    public void SavePlayerSO()
    {
        PlayerSOData data = new PlayerSOData();
        data.isNotNew = true;
        data.SetPlayerSO(GameManager.Instance.playerSO);
        data.skillPoint = SkillManager.Instance.skillPoint;
        data.learnedSkills = SkillManager.Instance.learnedSkills;
        string json = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(Application.persistentDataPath + @"\playerSOData.json", json);
    }
}
