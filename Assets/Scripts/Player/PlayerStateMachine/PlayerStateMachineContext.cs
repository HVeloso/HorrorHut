using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

[RequireComponent(typeof(PlayerInput))]
public class PlayerStateMachineContext : MonoBehaviour
{
    #region Parameters

    public float WalkingSpeed;
    public float RunningSpeed;
    public float RotationSpeed;

    public float DeltaTime { get; private set; }
    public float FixedDeltaTime { get; private set; }
    
    [HideInInspector] public float CurrentSpeed;
    public InputAction MoveAction { get; private set; }
    public InputAction RunAction { get; private set; }

    public float GravityMultiplier { get; private set; } = 3;
    public float Gravity { get; private set; } = -9.81f;
    [HideInInspector] public float GravityVelocity;

    public CharacterController CharacterController { get; private set; }
    public Transform NewCameraReference { get; private set; }
    public Transform CurrentCameraReference { get; private set; }

    [HideInInspector] public bool IsRunningButtonPressed { get; private set; } = false;
    [HideInInspector] public Vector2 MovementInputVector;
    [HideInInspector] public Vector3 ForwardRelativeToCamera;
    [HideInInspector] public Vector3 RightRelativeToCamera;
    [HideInInspector] public Vector3 MovementDirection;

    public Animator animator { get; private set; }

    #endregion

    #region States

    public PlayerBaseSuperState CurrentState { get; private set; }
    public PlayerIdleState idleState = new();
    public PlayerMovingState movingState = new();

    #endregion

    private void Awake()
    {
        StartCoroutine(WaitForCameraReference());

        MoveAction = GetComponent<PlayerInput>().actions.FindAction("Movement");
        RunAction = GetComponent<PlayerInput>().actions.FindAction("Run");

        CharacterController = GetComponent<CharacterController>();
        animator = transform.GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        ChangeState(idleState);
    }

    private void Update()
    {
        DeltaTime = Time.deltaTime * Time.timeScale;
        
        CurrentState.UpdateState(this);
    }

    private void FixedUpdate()
    {
        FixedDeltaTime = Time.deltaTime * Time.timeScale;

        CurrentState.FixedUpdateState(this);
    }

    public void ChangeState(PlayerBaseSuperState newState)
    {
        CurrentState?.ExitState(this);
        CurrentState = newState;
        CurrentState.EnterState(this);
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

    private IEnumerator WaitForCameraReference()
    {
        yield return new WaitUntil(() => NewCameraReference != null);
        CurrentCameraReference = NewCameraReference;
    }
}
