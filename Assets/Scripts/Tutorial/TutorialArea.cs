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
            // enemy ==> float, float <������ �߾� x ��,  ������ -1 ��.>
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
                UIManager.Instance.OpenUI<UITutorial>().OpenTutorial(sprites[explain], "���� ���� �ִ� ������ �ظ� ���� �����̸� �ı��� �� �ֽ��ϴ�.");
                break;
            case 1:
                UIManager.Instance.OpenUI<UITutorial>().OpenTutorial(sprites[explain], "������ �԰� �ִ� ���� �ظ� ���� �����̸� ��� ���� �� �� �ֽ��ϴ�.");
                break;
            case 2:
                UIManager.Instance.OpenUI<UITutorial>().OpenTutorial(sprites[explain], "Į�� ���� �����̸� ���� ��ī�ο� ������ ���� �մϴ�.");
                break;
            case 3:
                UIManager.Instance.OpenUI<UITutorial>().OpenTutorial(sprites[explain], "������ ��Ʈ�� ���� �����̸� ���� ������ ��� �մϴ�.");
                break;
            case 4:
                UIManager.Instance.OpenUI<UITutorial>().OpenTutorial(sprites[explain], "�Ǻ� ���� �����̸� ĳ���Ͱ� ���� ���� �Դϴ�..");
                break;
        }
    }

    private void CloseExplain()
    {
       UIManager.Instance.CloseUI<UITutorial>().CloseTutorial();
    }
}
