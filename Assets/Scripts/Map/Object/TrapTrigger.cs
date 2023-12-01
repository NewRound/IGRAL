using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    [SerializeField] private List<GameObject> Traps;


    private void Start()
    {
        foreach(GameObject obj in Traps)
        {
            if(obj.TryGetComponent<IObject>(out IObject trap))
            {

            }
            else
            {
                Traps.Remove(obj);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<InputController>(out InputController player))
        {
            foreach (GameObject obj in Traps)
            {
                obj.GetComponent<IObject>().Use();
            }
        }
    }
}
