using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerWalkingState : PlayerBaseState
{
    public override void ChangeSubState(PlayerBaseState newSubState) { }

    public override void EnterState(PlayerStateMachineContext context)
    {
        Debug.Log("Entrou no estado andar");
        context.CurrentSpeed = context.WalkingSpeed;
        context.PlayerAnimator.SetTrigger("Walk");
    }

    public override void FixedUpdateState(PlayerStateMachineContext context) { }

    public override void UpdateState(PlayerStateMachineContext context)
    {
        if (context.RunAction.triggered)
        {
            PlayerMovingState movingState = context.CurrentState as PlayerMovingState;
            context.CurrentState.ChangeSubState(movingState.runningState);
        }
    }
}
