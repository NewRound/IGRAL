using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    private Stack<EnemyController> enemyPoolStack = new Stack<EnemyController>();

    public EnemyController GetObject()
    {
        EnemyController select = null;

        if (enemyPoolStack.Count > 0)
            select = enemyPoolStack.Pop();
        else
        {
            GameObject go = Instantiate(enemy);
            go.transform.SetParent(transform);
            select = go.GetComponent<EnemyController>();
        }


        select.gameObject.SetActive(true);
        select.SetReturnPoolAction(PushObject);
        return select;
    }

    public void PushObject(EnemyController enemyController)
    {
        enemyPoolStack.Push(enemyController);
        enemyController.gameObject.SetActive(false);
    }
}
