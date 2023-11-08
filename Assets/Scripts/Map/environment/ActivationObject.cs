using UnityEngine;

public class ActivationObject : MonoBehaviour, IObject
{
    public void Use()
    {
        if(!this.gameObject.activeSelf)
            this.gameObject.SetActive(true);
    }
}
