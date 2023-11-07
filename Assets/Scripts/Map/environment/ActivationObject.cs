using UnityEngine;

public class ActivationObject : MonoBehaviour, IObject
{
    public void Use()
    {
        this.gameObject.SetActive(true);
    }
}
