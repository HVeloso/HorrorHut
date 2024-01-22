using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovingState : PlayerBaseSuperState
{
    public PlayerWalkingState walkingState = new();
    public PlayerRunningState runningState = new();

    public override void EnterState(PlayerStateMachineContext context)
    {
        ChangeSubState(context, walkingState);
    }

    public override void FixedUpdateState(PlayerStateMachineContext context)
    {
        CurrentSubState.FixedUpdateSubState(context, this);

        if (context.MovementInputVector.magnitude <= 0)
        {
            context.ChangeState(context.idleState);
        }

        MovePlayer(context);
    }

    public override void UpdateState(PlayerStateMachineContext context)
    {
        CurrentSubState.UpdateSubState(context, this);
    }

    public override void ChangeSubState(PlayerStateMachineContext context, PlayerBaseSubState newSubState)
    {
        CurrentSubState = newSubState;
        CurrentSubState.EnterSubState(context, this);
    }

    #region Moving Methods

    private void MovePlayer(PlayerStateMachineContext context)
    {
        context.MovementInputVector = context.MoveAction.ReadValue<Vector2>();

        ApplyMovement(context);
        ApplyRotationOnPlayerEntity(context);
        ApplyGravity(context);

        context.CharacterController.Move(context.CurrentSpeed * context.FixedDeltaTime * context.MovementDirection);
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

        context.MovementDirection.y = context.GravityVelocity;
    }

    private void ApplyMovement(PlayerStateMachineContext context)
    {
        context.ForwardRelativeToCamera = context.CurrentCameraReference.forward * context.MovementInputVector.y;
        context.RightRelativeToCamera = context.CurrentCameraReference.right * context.MovementInputVector.x;

        context.MovementDirection = context.ForwardRelativeToCamera + context.RightRelativeToCamera;
        context.MovementDirection.Normalize();
    }

    private void ApplyRotationOnPlayerEntity(PlayerStateMachineContext context)
    {
        Vector3 dir = context.MovementDirection;
        dir.y = 0f;
        if (dir.magnitude > 0f)
            context.PlayerEntity.forward = Vector3.Slerp(context.PlayerEntity.forward, dir, context.FixedDeltaTime * context.RotationSpeed);
    }

    #endregion
}
