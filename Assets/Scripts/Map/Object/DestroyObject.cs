using System;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] private GameObject _createObj;
    [SerializeField] private LayerMask _canBeDestroyBy;
    private PlayerAppearanceController _player;

    private void Start()
    {
        _player = GameManager.Instance.PlayerAppearanceController;
    }

    private void CreateObj()
    {
        Debug.Log(" �ٴ� ���� ");
        Instantiate(_createObj, transform.position, Quaternion.identity);

        // �ö� �� �ִ� ������������ �����Ǵ� ���������
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        if (!(_canBeDestroyBy.value == ( _canBeDestroyBy.value | (1 << collision.gameObject.layer)))) return;
        
        if (CheckMutantType())
        {
            DestroyObj();
            CreateObj();
        }        
    }

    // �÷��̾��� ���� ���� & ���� ���̻��� Ȯ��
    // OnCollsionEnter > ����, ��ġ���·� ������ ���� �ʾƵ� ������ ���� �۵� 
    // ������ �� ���� �۵��ϰ� �ϴ� ���?


    private void DestroyObj()
    {
        // TODO

        // �μ����� ȿ��, ȿ����
        // ����
    }

    private bool CheckMutantType()
    {
        if (_player.mutantType == MutantType.Stone) return true;

        return false;
    }
}
