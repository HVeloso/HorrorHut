using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovingState : PlayerBaseState
{
    private PlayerBaseState currentSubState;
    public PlayerWalkingState walkingState = new();
    public PlayerRunningState runningState = new();

    private PlayerStateMachineContext _context;

    public override void EnterState(PlayerStateMachineContext context)
    {
        _context = context;
        ChangeSubState(walkingState);
        Debug.Log("Entrou no estado de movimento");
    }

    public override void FixedUpdateState(PlayerStateMachineContext context)
    {
        currentSubState.FixedUpdateState(context);

        MovePlayer(context);
    }

    public override void UpdateState(PlayerStateMachineContext context)
    {
        if (context.MovementInputVector.magnitude <= 0)
        {
            context.ChangeState(context.idleState);
        }

        currentSubState.UpdateState(context);
    }

    public override void ChangeSubState(PlayerBaseState newSubState)
    {
        currentSubState = newSubState;
        currentSubState.EnterState(_context);
    }

    private void MovePlayer(PlayerStateMachineContext context)
    {
        context.MovementInputVector = context.MoveAction.ReadValue<Vector2>();

        ApplyMovement(context);
        ApplyGravity(context);

        context.CharacterController.Move(context.FixedDeltaTime * context.MovementDirection);
    }

    private void ApplyGravity(PlayerStateMachineContext context)
    {
        if (context.CharacterController.isGrounded)
        {
            context.GravityVelocity = -1.0f;
        }
        else
        {
            Debug.Log("Movendo");

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
        context.MovementDirection *= context.CurrentSpeed;
    }
}
