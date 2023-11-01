using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    private List<GameObject> enemys = new List<GameObject>();
    private bool PlayerInArea = false;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            enemys.Add(other.gameObject);
            if(PlayerInArea)
            {
                // other ==> chaing State
            }
        }

        if (other.gameObject.tag == "Player")
        {
            PlayerInArea = true;
            foreach(GameObject enemy in enemys)
            {
                // enemy ==> chaing State
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemys.Remove(other.gameObject);
        }

        if (other.gameObject.tag == "Player")
        {
            PlayerInArea = false;
            foreach (GameObject enemy in enemys)
            {
                // enemy ==> Idle State
            }
        }
    }

}
