using UnityEngine.SceneManagement;

public class NextStageObject : InteractiveObject
{
    public override void Use()
    {
        if (CheckCondition())
        {
            int stage = GameManager.Instance.currentStage;
            stage++;
            GameManager.Instance.currentStage = stage;
            string sceneName = SceneManager.GetActiveScene().name;
            LoadSceneManager.Instance.LoadScene(sceneName);
        }
    }

    public virtual bool CheckCondition()
    {
        return true;
    }
}
