using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseSubState
{
    public abstract void EnterSubState(PlayerStateMachineContext context, PlayerBaseSuperState superState);
    public abstract void ExitSubState(PlayerStateMachineContext context, PlayerBaseSuperState superState);
    public abstract void UpdateSubState(PlayerStateMachineContext context, PlayerBaseSuperState superState);
    public abstract void FixedUpdateSubState(PlayerStateMachineContext context, PlayerBaseSuperState superState);
}
