using UnityEngine;
using UnityEngine.UI;

public class UITopButton : MonoBehaviour
{
    [SerializeField] private Button _option;
    [SerializeField] private Button _inventory;
    [SerializeField] private Button _skillTree;

    private Color _alpha;
    public bool _skillTreeBlink;
    private float _blinkSpeed;
    private float _blinkTime;

    private void Awake()
    {
        _option.onClick.AddListener(OpenOption);
        _inventory.onClick.AddListener(OpenInventory);
        _skillTree.onClick.AddListener(OpenSkillTree);

        _alpha = _skillTree.image.color;
        _blinkSpeed = 5f;
    }

    private void Update()
    {
        _blinkTime += Time.deltaTime;

        if (_skillTreeBlink)
        {
            _alpha.a = (Mathf.Cos(_blinkTime * _blinkSpeed) + 1) * GlobalValues.HALF;
            _skillTree.image.color = _alpha;
        }

        else
        {
            AlphaInit();
        }
    }

    private void AlphaInit()
    {
        _alpha.a = 1f;
        _skillTree.image.color = _alpha;
    }

    private void OpenOption()
    {
        UIManager.Instance.OpenUI<UIOption>();
        AudioManager.Instance.PlaySFX(SFXType.Click);
    }

    private void OpenInventory()
    {
        UIInventory.Instance.OpenInventory();
        AudioManager.Instance.PlaySFX(SFXType.Click);
    }

    private void OpenSkillTree()
    {
        UISkillTree.Instance.OpenSkillTree();
        AudioManager.Instance.PlaySFX(SFXType.Click);
    }
}
