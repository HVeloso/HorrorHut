using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunningState : PlayerBaseSubState
{
    public override void EnterSubState(PlayerStateMachineContext context, PlayerBaseSuperState superState)
    {
        context.CurrentSpeed = context.RunningSpeed;
        context.animator.SetTrigger("RUN");
    }

    public override void ExitSubState(PlayerStateMachineContext context, PlayerBaseSuperState superState) { }

    public override void FixedUpdateSubState(PlayerStateMachineContext context, PlayerBaseSuperState superState) { }

    public override void UpdateSubState(PlayerStateMachineContext context, PlayerBaseSuperState superState)
    {
        if (context.RunAction.triggered)
        {
            PlayerMovingState movingState = superState as PlayerMovingState;
            movingState.ChangeSubState(context, movingState.walkingState);
        }
    }
}
