using System;

public interface IDamageable
{
    public event Action DieAction;
    void Damaged(float damage);   
    void Recovery(float damage);
}
