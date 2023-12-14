using UnityEngine;

public class CharacterWeapon : Weapon
{
    [SerializeField] protected float rayOffsetY = 0.8f;
    [SerializeField] protected float rayOffsetYMod = 0.5f;
    [SerializeField] protected float rayOffsetX = 0.2f;
    protected Transform modelTrans;
    protected Transform myTrans;
    protected LayerMask layer;
    private float _rayOffsetX;

    protected Vector3 UpdateRayOffset()
    {
        Vector3 offsetVec = myTrans.position;
        _rayOffsetX = modelTrans.forward.x > 0 ? -rayOffsetX : rayOffsetX;
        offsetVec.x += _rayOffsetX;
        offsetVec.y += rayOffsetY;
        return offsetVec;
    }
}