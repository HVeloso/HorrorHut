using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerStateMachineContext context) { Debug.Log("Entoru no estado in�rte"); }

    public override void FixedUpdateState(PlayerStateMachineContext context) { }
    public override void UpdateState(PlayerStateMachineContext context)
    {
        SwitchState(context);
    }

    protected override void SwitchState(PlayerStateMachineContext context)
    {
        if (context.MoveAction.ReadValue<Vector2>().magnitude > 0)
        {
            context.ChangeState(context.walkingState);
        }
    }
}
