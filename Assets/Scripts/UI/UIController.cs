using UnityEngine;
using UnityEngine.UI;

public class UIController : InputController
{
    [SerializeField] private VariableJoystick variableJoystick;
    [SerializeField] private Button _healing;
    [SerializeField] private Button _jump;
    [SerializeField] private Button _dash;
    [SerializeField] private Button _skill;
    [SerializeField] private Button _attack;
    [SerializeField] private Button _interaction;
    [SerializeField] private Button _pickup;
    [SerializeField] private Button _item;

    protected override void Awake()
    {
        base.Awake();
    }

    private void FixedUpdate()
    {
        Vector2 direction = Vector2.up * variableJoystick.Vertical + Vector2.right * variableJoystick.Horizontal;
        Debug.Log(direction);
        CallMoveAction(direction);
    }


}
