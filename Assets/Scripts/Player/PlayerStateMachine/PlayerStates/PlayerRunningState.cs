using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunningState : PlayerBaseState
{
    public override void ChangeSubState(PlayerBaseState newSubState) { }

    public override void EnterState(PlayerStateMachineContext context)
    {
        Debug.Log("Entrou no estado correndo");
        context.CurrentSpeed = context.runningSpeed;
    }

    public override void FixedUpdateState(PlayerStateMachineContext context) { }

    public override void UpdateState(PlayerStateMachineContext context)
    {
        if (context.RunAction.triggered)
        {
            PlayerMovingState movingState = context.CurrentState as PlayerMovingState;
            context.CurrentState.ChangeSubState(movingState.walkingState);
        }
    }
}
