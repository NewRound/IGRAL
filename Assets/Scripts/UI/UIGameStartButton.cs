using UnityEngine;
using UnityEngine.UI;

public class UIGameStartButton : MonoBehaviour
{
    [SerializeField] private Button _startButton;

    private void Awake()
    {
        //Debug.Log(UIManager.Instance);
        Debug.Log(AudioManager.Instance);
        Debug.Log(ItemManager.Instance);

        Debug.Log(LoadSceneManager.Instance);
        _startButton.onClick.AddListener(OnStartButton);
    }

    private void OnStartButton()
    {
        LoadSceneManager.Instance.LoadScene("SampleScene");
    }
}
