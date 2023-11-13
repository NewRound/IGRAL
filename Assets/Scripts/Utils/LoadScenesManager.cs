using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : CustomSingleton<LoadSceneManager>
{
    public void LoadScene(string sceneName)
    {
        if (sceneName == null)
            return;

        PlayerPrefs.SetString("Scene", sceneName);
        SceneManager.LoadScene("LoadScene");
    }
}
