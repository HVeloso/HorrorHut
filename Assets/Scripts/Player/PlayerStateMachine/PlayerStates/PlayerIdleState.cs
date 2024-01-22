using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseSuperState
{
    public override void ChangeSubState(PlayerStateMachineContext context, PlayerBaseSubState newSubState) { }

    public override void EnterState(PlayerStateMachineContext context)
    {
        context.UpdateCurrentCamera();
    }

    public override void FixedUpdateState(PlayerStateMachineContext context) { }
    public override void UpdateState(PlayerStateMachineContext context)
    {
        if (context.MoveAction.ReadValue<Vector2>().magnitude > 0)
        {
            context.ChangeState(context.movingState);
        }
    }
}
