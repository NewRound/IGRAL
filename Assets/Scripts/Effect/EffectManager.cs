using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    Explosion,
    damaged   
}

public class EffectManager : CustomSingleton<EffectManager>
{
    [SerializeField] private GameObject[] effects;

    private List<GameObject>[] effectList;

    private void Awake()
    {
        effectList = new List<GameObject>[effects.Length];

        for (int i = 0; i < effects.Length; i++)
        {
            effectList[i] = new List<GameObject>();
        }
    }

    public void ShowEffect(Vector3 pos, EffectType type)
    {
        GameObject effect = GetEffect(type);
        effect.transform.position = pos;
    }
    
    public GameObject GetEffect(EffectType type)
    {
        GameObject selectEffect = null;

        foreach (GameObject effect in effectList[(int)type])
        {
            if (!effect.activeSelf)
            {
                selectEffect = effect;
                selectEffect.SetActive(true);
                break;
            }
        }

        if (!selectEffect)
        {
            selectEffect = Instantiate(effects[(int)type],transform);
            effectList[(int)type].Add(selectEffect);
        }

        return selectEffect;
    }
}
