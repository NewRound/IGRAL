using System.Collections.Generic;
using UnityEngine;


public enum MutantType
{
    None,
    Stone,
    Blade,
    Sheld,
    Skin,
}

public class PlayerAppearanceController : MonoBehaviour
{
    public MutantType mutantType { get; private set; }

    [SerializeField] public List<GameObject> Mutant_None;

    [SerializeField] public List<GameObject> Mutant_Stone;
    [SerializeField] public List<GameObject> Mutant_Blade;
    [SerializeField] public List<GameObject> Mutant_Sheld;
    [SerializeField] public List<GameObject> Mutant_SkinBaby;

    private void Awake()
    {
        mutantType = MutantType.None;
        OnOffMutant(MutantType.None, true);
        OnOffMutant(MutantType.Stone, false);
        OnOffMutant(MutantType.Blade, false);
        OnOffMutant(MutantType.Sheld, false);
        OnOffMutant(MutantType.Skin, false);
    }

    public void ChangeMutant(MutantType type)
    {
        if(type == MutantType.Skin || mutantType == MutantType.Sheld)
        {
            if(mutantType != MutantType.None)
            {
                OnOffMutant(mutantType, false);
            }
        }
        else
        {
            OnOffMutant(mutantType, false);
        }

        mutantType = type;

        OnOffMutant(mutantType, true);
    }

    private void OnOffMutant(MutantType type, bool OnOff)
    {
        List<GameObject> Mutant;
        switch (type)
        {
            case MutantType.None:
                Mutant = Mutant_None;

                break;
            case MutantType.Stone:
                Mutant = Mutant_Stone;

                break;
            case MutantType.Blade:
                Mutant = Mutant_Blade;

                break;
            case MutantType.Sheld:
                Mutant = Mutant_Sheld;

                break;
            case MutantType.Skin:
                Mutant = Mutant_SkinBaby;

                break;
            default:
                return;
        }
        foreach (GameObject obj in Mutant)
        {
            obj.SetActive(OnOff);
        }
    }
}
