using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BulletBase
{
    public void Move(bool isRight)
    {
        Vector3 direction = isRight ? transform.right : -transform.right;
        rigid.AddForce(direction * 10f, ForceMode.Impulse);
    }
}
