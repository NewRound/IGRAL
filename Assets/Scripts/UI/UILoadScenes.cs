using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    private string nextScene;
    [SerializeField] private Image _progressBar;

    private void Start()
    {
        SkillManager.Instance.StopAllSKill();
        nextScene = PlayerPrefs.GetString("Scene");
        if(nextScene == null)
        {
            SceneManager.LoadScene("IntroScenes");
        }
        StartCoroutine(LoadScenes());
    }

    IEnumerator LoadScenes()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                _progressBar.fillAmount = Mathf.Lerp(_progressBar.fillAmount, op.progress, timer);
                if (_progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                _progressBar.fillAmount = Mathf.Lerp(_progressBar.fillAmount, 1f, timer);
                if (_progressBar.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
