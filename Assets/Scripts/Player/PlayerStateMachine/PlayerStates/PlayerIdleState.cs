using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void ChangeSubState(PlayerBaseState newSubState) { }

    public override void EnterState(PlayerStateMachineContext context)
    {
        Debug.Log("Entrou no estado inérte");
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
