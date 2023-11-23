using System.Collections.Generic;
using UnityEngine;

public class TutorialArea : MonoBehaviour
{
    [Header("EnemyCount")]
    [SerializeField] public int enemyCount;

    [Header("AreaData")]
    [SerializeField] public float size;
    [SerializeField] public Vector3 position;

    [SerializeField] public List<EnemyController> enemys = new List<EnemyController>();
    private bool PlayerInArea = false;

    [SerializeField] private LayerMask _player;
    [SerializeField] private LayerMask _enemy;

    [SerializeField] private int explain;
    [SerializeField] private Sprite[] sprites;

    private void Start()
    {
        position = transform.position;
        size = (6 * size) - 1;

        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemy = ObjectPoolingManager.Instance.GetEnemyTutorial(i);
            enemy.transform.position = position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_enemy.value == (_enemy.value | (1 << other.gameObject.layer)))
        {
            enemys.Add(other.GetComponent<EnemyController>());
            // enemy ==> float, float <발판의 중앙 x 값,  길이의 -1 값.>
            SendAreaInfo(other.gameObject);

            if (PlayerInArea)
            {
                enemys[enemys.Count - 1].StateMachine.SetIsTracing(true);
            }
        }

        if (_player.value == (_player.value | (1 << other.gameObject.layer)))
        {
            PlayerInArea = true;
            foreach (EnemyController enemy in enemys)
            {
                enemy.StateMachine.SetIsTracing(true);
            }
            OpenExplain();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_enemy.value == (_enemy.value | (1 << other.gameObject.layer)))
        {
            enemys.Remove(other.GetComponent<EnemyController>());
        }

        if (_player.value == (_player.value | (1 << other.gameObject.layer)))
        {
            PlayerInArea = false;
            foreach (EnemyController enemy in enemys)
            {
                enemy.StateMachine.SetIsTracing(false);
            }
            CloseExplain();
        }
    }

    private void SendAreaInfo(GameObject enemy)
    {
        EnemyStateMachine enemyStateMachine = enemy.GetComponent<EnemyController>().StateMachine;
        enemyStateMachine.SetAreaData(transform.position.x, size);
        enemyStateMachine.Init();
    }

    private void OpenExplain()
    {
        switch (explain)
        {
            case 0:
                UIManager.Instance.OpenUI<UITutorial>().OpenTutorial(sprites[explain], "길을 막고 있는 바위는 해머 변이 상태이면 파괴할 수 있습니다.");
                break;
            case 1:
                UIManager.Instance.OpenUI<UITutorial>().OpenTutorial(sprites[explain], "갑옷을 입고 있는 적은 해머 변이 상태이면 대미 지을 줄 수 있습니다.");
                break;
            case 2:
                UIManager.Instance.OpenUI<UITutorial>().OpenTutorial(sprites[explain], "칼날 변이 상태이면 때는 날카로운 공격이 가능 합니다.");
                break;
            case 3:
                UIManager.Instance.OpenUI<UITutorial>().OpenTutorial(sprites[explain], "사이코 메트릭 변이 상태이면 적의 공격을 방어 합니다.");
                break;
            case 4:
                UIManager.Instance.OpenUI<UITutorial>().OpenTutorial(sprites[explain], "피부 변이 상태이면 캐릭터가 은신 상태 입니다..");
                break;
        }
    }

    private void CloseExplain()
    {
       UIManager.Instance.CloseUI<UITutorial>().CloseTutorial();
    }
}
