using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseSuperState
{
    public PlayerBaseSubState CurrentSubState { get; protected set; }

    public abstract void EnterState(PlayerStateMachineContext context);
    public abstract void UpdateState(PlayerStateMachineContext context);
    public abstract void FixedUpdateState(PlayerStateMachineContext context);
    public abstract void ChangeSubState(PlayerStateMachineContext context, PlayerBaseSubState newSubState);
}
