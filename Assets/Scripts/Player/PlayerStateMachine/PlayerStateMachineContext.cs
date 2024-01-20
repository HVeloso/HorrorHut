using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerStateMachineContext : MonoBehaviour
{
    #region Parameters

    [Header("Forward speed settings")]
    public float walkingSpeed;
    public float runningSpeed;

    public float DeltaTime { get; private set; }

    [HideInInspector] public float CurrentSpeed;
    public InputAction MoveAction { get; private set; }

    public float GravityMultiplier { get; private set; } = 3;
    public float Gravity { get; private set; } = -9.81f;
    public float GravityVelocity;

    public CharacterController CharacterController { get; private set; }
    public Transform NewCameraReference { get; private set; }
    public Transform CurrentCameraReference { get; private set; }

    public Vector2 MovementInputVector;
    public Vector3 ForwardRelativeToCamera;
    public Vector3 RightRelativeToCamera;
    public Vector3 MovimentionDirection;

    #endregion

    #region States

    private PlayerBaseState currentState;
    public PlayerIdleState idleState = new();
    public PlayerWalkingState walkingState = new();

    #endregion

    private void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        MoveAction = GetComponent<PlayerInput>().actions.FindAction("Movement");
    }

    private void Start()
    {
        ChangeState(idleState);
        StartCoroutine(GetFirstCameraReference());
    }

    private void Update()
    {
        DeltaTime = Time.deltaTime * Time.timeScale;
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

    public void UpdateCameraReference(Transform cameraReference)
    {
        NewCameraReference = cameraReference;
    }

    public void UpdateCurrentCamera()
    {
        if (NewCameraReference != null && NewCameraReference != CurrentCameraReference)
            CurrentCameraReference = NewCameraReference;
    }

    private IEnumerator GetFirstCameraReference()
    {
        yield return new WaitUntil(() => NewCameraReference != null);
        CurrentCameraReference = NewCameraReference;
    }
}
