using UnityEngine;

enum Scenes
{
    LobbyScene
    , SampleScene
    , TutorialMap
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
            case Scenes.TutorialMap:
                LoadSceneManager.Instance.LoadScene("TutorialMap");
                break;
        }
    }
}
