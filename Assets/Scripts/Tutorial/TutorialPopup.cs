using UnityEngine;

public class TutorialPopup : MonoBehaviour
{
    private void Start()
    {
        Invoke("Popup", 0.5f);

    }

    private void Popup()
    {
        UIManager.Instance.TutorialPopup();
    }
}
