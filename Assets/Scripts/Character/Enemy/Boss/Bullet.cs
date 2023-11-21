using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _rigid;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    public void Move(bool isRight)
    {
        Vector3 direction = isRight ? transform.right : -transform.right;
        _rigid.AddForce(direction * 10f, ForceMode.Impulse);
    }

   

}
