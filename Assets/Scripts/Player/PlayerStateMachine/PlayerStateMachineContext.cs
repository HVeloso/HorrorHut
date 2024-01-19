using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerStateMachineContext : MonoBehaviour
{
    #region Parameters

    [Header("Forward speed settings")]
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float runningSpeed;

    public float DeltaTime { get; private set; }

    public float CurrentSpeed;

    public InputAction MoveAction;

    #endregion

    #region States

    private PlayerBaseState currentState;
    public PlayerIdleState idleState = new();
    public PlayerWalkingState walkingState = new();

    #endregion

    private void Awake()
    {
        MoveAction = GetComponent<PlayerInput>().actions.FindAction("Movement");
    }

    private void Start()
    {
        ChangeState(idleState);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    public void ChangeState(PlayerBaseState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }
}
