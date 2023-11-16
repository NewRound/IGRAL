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

    private void OnDisable()
    {
        bg.alpha = 0;   
    }

    private void OnEnable()
    {
        bg.DOFade(1f, 2f);
    }

    private void OnButton()
    {
        LoadSceneManager.Instance.LoadScene("IntroScenes");
    }
}
