using UnityEngine;

public class HideObject : MonoBehaviour, IObject
{
    public void Use()
    {
        this.gameObject.SetActive(false);
    }
}
