using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private CanvasGroup bg;

    private void Start()
    {
        button.onClick.AddListener(OnButton);
    }

    public void Open()
    {
        bg.alpha = 0;
        bg.DOFade(1f, 5f);
    }

    private void OnButton()
    {
        LoadSceneManager.Instance.LoadScene("LobbyScene");
    }
}
