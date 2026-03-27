using Unity.VisualScripting;
using UnityEngine;
using static EnemyStateMachine;

// This is the state machine for the Player. It defines the different states that the enemy can be in and manages the transitions between those states.
public class PlayerStateMachine : StateManager<PlayerStateMachine.EPlayerState>
{ 

    public enum EPlayerState
    {
        //States
        Standard,
        Interacting,
        Hiding,
        Doomed,
        Working,
    }

    private PlayerStateContext _pContext;


    //Variables

    [Header("References")]
    public Interactor interactorRef;
    public HeadBobbing headBobbingRef;

    //copies of those from Interactor script
    [Header("Interactor")]
    public Transform interactorSource;
    public float interactionRange;
    public LayerMask interactionMask;

    [Header("Player Settings")]
    public float playerWalkSpeed = 5f;
    public float playerRunMultiplier = 1.5f;
    public float playerJumpForce = 2f;
    public float groundCheckDistance = 1.5f;

    [Header("Character Controller")]
    //Character Controller reference
    public CharacterController characterController;
    public bool isGrounded;
    public float gravity;
    public Vector3 velocity;
    public float verticalRotation = 0f;

    public Rigidbody rb;
    public GameObject playerGameObject;

    [Header("Camera")]
    public float mouseSensitivityX = 1f;
    public float mouseSensitivityY = 1f;
    public float minLookAngleY = -90f;
    public float maxLookAngleY = 90f;
    public Transform playerCamera;
    public GameObject camPivotRef;

    [Header("Camera Tilting")]
    public float zTiltAmount;
    public float tiltStartSpeed;
    public float tiltEndSpeed;
    private float zCurrentTilt = 0f;
    private float zTargetTilt = 0f;
    public float zSmoothTilt;

    public float xTiltAmount;
    private float xCurrentTilt = 0f;
    private float xTargetTilt = 0f;
    public float xSmoothTilt;

    [Header("States and Extras Variables")]
    public bool changeToWorkState;



    public void Awake()
    {
        _pContext = new PlayerStateContext(interactorRef, headBobbingRef, interactorSource, interactionRange, interactionMask, playerWalkSpeed, playerRunMultiplier, playerJumpForce, groundCheckDistance, characterController, isGrounded, gravity, velocity, verticalRotation, rb, playerGameObject, mouseSensitivityX, mouseSensitivityY, minLookAngleY, maxLookAngleY, playerCamera, camPivotRef,
        zTiltAmount, tiltStartSpeed, tiltEndSpeed, zCurrentTilt, zTargetTilt, zSmoothTilt, xTiltAmount, xCurrentTilt, xTargetTilt, xSmoothTilt, changeToWorkState);
        InitialiseStates();     
    }

    private void InitialiseStates()
    {
        //Add the states to the state machine here. The key is the enum value and the value is the state class that corresponds to that enum value. Then set state

        States.Add(EPlayerState.Standard, new StandardState(_pContext, EPlayerState.Standard));
        States.Add(EPlayerState.Interacting, new InteractingState(_pContext, EPlayerState.Interacting));
        States.Add(EPlayerState.Hiding, new HidingState(_pContext, EPlayerState.Hiding));
        States.Add(EPlayerState.Doomed, new DoomedState(_pContext, EPlayerState.Doomed));
        States.Add(EPlayerState.Working, new WorkingState(_pContext, EPlayerState.Working));

        CurrentState = States[EPlayerState.Standard];
    }
}
