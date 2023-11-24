using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{
    protected Rigidbody rigid;

    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
}
