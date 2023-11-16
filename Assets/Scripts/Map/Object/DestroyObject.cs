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
        Debug.Log(" 바닥 생성 ");
        Instantiate(_createObj, transform.position, Quaternion.identity);

        // 올라갈 수 있는 발판형식으로 생성되는 돌무더기들
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

    // 플레이어의 공격 감지 & 현재 변이상태 확인
    // OnCollsionEnter > 오류, 망치상태로 공격을 하지 않아도 가까이 가면 작동 
    // 공격을 할 때만 작동하게 하는 방법?


    private void DestroyObj()
    {
        // TODO

        // 부서지는 효과, 효과음
        // 파편
    }

    private bool CheckMutantType()
    {
        if (_player.mutantType == MutantType.Stone) return true;

        return false;
    }
}
