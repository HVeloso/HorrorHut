using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState
{
    public abstract void EnterState(PlayerStateMachineContext context);
    public abstract void UpdateState(PlayerStateMachineContext context);
    public abstract void FixedUpdateState(PlayerStateMachineContext context);
}
