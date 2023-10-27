using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IObject
{
    void Use();
}

public interface IMovingObject : IObject
{
    bool CheckCondition();
}

public interface IInteractionObject : IObject
{
    bool CheckCondition();
}

public interface IDamagableObject : IObject
{
    void OnTriggerEnter(Collider other);
}

public interface ITriggerObject : IObject
{
    void OnTriggerEnter(Collider other);
}

public interface IInteractable
{
    string GetInteractPrompt();
    void OnInteract();
}
