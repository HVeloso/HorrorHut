using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerWalkingState : PlayerBaseState
{
    public override void EnterState(PlayerStateMachineContext context)
    {
        Debug.Log("Entoru no estado andar");
        context.CurrentSpeed = context.walkingSpeed;
    }

    public override void FixedUpdateState(PlayerStateMachineContext context)
    {
        MovePlayer(context);
    }

    public override void UpdateState(PlayerStateMachineContext context)
    {
        if (context.MoveAction.ReadValue<Vector2>().magnitude <= 0)
        {
            context.ChangeState(context.idleState);
        }
    }

    private void MovePlayer(PlayerStateMachineContext context)
    {
        context.MovementInputVector = context.MoveAction.ReadValue<Vector2>();
        context.MovimentionDirection = Vector3.zero;

        ApplyMovement(context);
        ApplyGravity(context);

        context.CharacterController.Move(context.CurrentSpeed * context.DeltaTime * context.MovimentionDirection);
    }

    private void ApplyGravity(PlayerStateMachineContext context)
    {
        if (context.CharacterController.isGrounded)
        {
            context.GravityVelocity = -1.0f;
        }
        else
        {
            context.GravityVelocity += context.Gravity * context.GravityMultiplier;
        }

        context.MovimentionDirection.y = context.GravityVelocity;
    }

    private void ApplyMovement(PlayerStateMachineContext context)
    {
        context.ForwardRelativeToCamera = context.CurrentCameraReference.forward;
        context.ForwardRelativeToCamera *= context.MovementInputVector.y;

        context.ForwardRelativeToCamera = context.CurrentCameraReference.right * context.MovementInputVector.x;
        context.MovimentionDirection = context.ForwardRelativeToCamera + context.RightRelativeToCamera;
        context.MovimentionDirection.Normalize();
    }
}
