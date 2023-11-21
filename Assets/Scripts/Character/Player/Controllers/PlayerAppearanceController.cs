using System.Collections;
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

    [SerializeField] public Animator animator;

    private PlayerEffectController _playerEffectController;

    private void Start()
    {
        _playerEffectController = GameManager.Instance.PlayerInputController.EffectController;
        _playerEffectController.EffectDataHandler.SetDissolveMaterial(Mutant_Stone[0].GetComponentInChildren<MeshRenderer>().sharedMaterial);
        _playerEffectController.ResetViewerData();

        mutantType = MutantType.None;
        OnOffMutant(MutantType.None, true);
        OnOffMutant(MutantType.Stone, false);
        OnOffMutant(MutantType.Blade, false);
        OnOffMutant(MutantType.Sheld, false);
        OnOffMutant(MutantType.Skin, false);
    }

    public void ChangeMutant(MutantType type)
    {
        if(type == MutantType.Skin || type == MutantType.Sheld)
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
        //animator.SetInteger("MutantType", (int)type);

        mutantType = type;
        GameManager.Instance.StatHandler.UpdateSkillStat(type);
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


        if (type != MutantType.None && type != MutantType.Skin)
        {
            if (OnOff)
                _playerEffectController.AppearWeapon(Mutant);
            else
                _playerEffectController.DisappearWeapon(Mutant);
        }
        else
        {
            if (OnOff)
                _playerEffectController.AppearWeaponWithoutDissolve(Mutant);
            else
                _playerEffectController.DisappearWeaponWithoutDissolve(Mutant);
        }
    }
}
