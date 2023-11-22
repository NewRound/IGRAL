using UnityEngine;

enum Scenes
{
    LobbyScene,
    SampleScene,
    LoadScene,
}

public class LoadScenePoint : InteractiveObject
{
    [SerializeField] private Scenes scenes;

    public override void Use()
    {
        GameManager.Instance.BackUpPlayerSO();

        switch (scenes)
        {
            case Scenes.LobbyScene:
                LoadSceneManager.Instance.LoadScene("LobbyScene");
                break;
            case Scenes.SampleScene:
                LoadSceneManager.Instance.LoadScene("SampleScene");
                break;
            case Scenes.LoadScene:
                LoadSceneManager.Instance.LoadScene("LoadScene");
                break;
            default:
                break;
        }
    }
}
