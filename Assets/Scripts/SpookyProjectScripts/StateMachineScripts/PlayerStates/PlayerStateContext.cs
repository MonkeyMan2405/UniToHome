using UnityEngine;

public class PlayerStateContext
{

    [Header("References")]
    public Interactor interactorRef;
    public HeadBobbing headBobbingRef;



    [Header("Blinds Variables")]
    public Transform blinds;



    [Header("Working Variables")]
    public Transform newCamPos;
    public Camera workCamera;
    //passed through context for transition state to check what transition action to take
    public int transitionIdentifier;



    [Header("Interactor")]
    public Transform interactorSource;
    public float interactionRange;
    public LayerMask interactionMask;



    [Header("Player Settings")]
    public float playerWalkSpeed = 3f;
    public float playerRunMultiplier = 1.5f;
    public float playerJumpForce = 2f;
    public float groundCheckDistance = 1.5f;



    [Header("CharacterController")]
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
    public Camera actualPlayerCamera;



    [Header("Camera Tilting")]
    public float zTiltAmount;
    public float tiltStartSpeed;
    public float tiltEndSpeed;
    public float zCurrentTilt = 0f;
    public float zTargetTilt = 0f;
    public float zSmoothTilt;



    public float xTiltAmount;
    public float xCurrentTilt = 0f;
    public float xTargetTilt = 0f;
    public float xSmoothTilt;



    [Header("States and Extras Variables")]
    public bool changeToWorkState;




    public PlayerStateContext
    (
        Interactor interactorRef,
        HeadBobbing headBobbingRef,

        Transform blinds,

        Transform newCamPos,
        Camera workCamera,
        int transitionIdentifier,

        Transform interactorSource,
        float interactionRange,
        LayerMask interactionMask,

        float playerWalkSpeed = 5f,
        float playerRunMultiplier = 1.5f,
        float playerJumpForce = 2f,
        float groundCheckDistance = 1.5f,
        CharacterController characterController = null,
        bool isGrounded = true,
        float gravity = 0f,
        Vector3 velocity = default,
        float verticalRotation = 0f,

        Rigidbody rb = null,
        GameObject playerGameObject = null,
        
        float mouseSensitivityX = 1f,
        float mouseSensitivityY = 1f,
        float minLookAngleY = -90f,
        float maxLookAngleY = 90f,
        Transform playerCamera = null,
        GameObject camPivotRef = null,
        Camera actualPlayerCamera = null,


        float zTiltAmount = 0f,
        float tiltStartSpeed = 0f,
        float tiltEndSpeed = 0f,
        float zCurrentTilt = 0f,
        float zTargetTilt = 0f,
        float zSmoothTilt = 0f,

        float xTiltAmount = 0f,
        float xCurrentTilt = 0f,
        float xTargetTilt = 0f,
        float xSmoothTilt = 0f,

        bool changeToWorkState = false
    )


    {
        // Initialise the context with nescessary variables for the player state machine

        this.changeToWorkState = changeToWorkState;

        this.blinds = blinds;

        this.interactorRef = interactorRef;
        this.headBobbingRef = headBobbingRef;
        this.transitionIdentifier = transitionIdentifier;

        this.newCamPos = newCamPos;
        this.workCamera = workCamera;

        this.interactorSource = interactorSource;
        this.interactionRange = interactionRange;
        this.interactionMask = interactionMask;

        this.playerWalkSpeed = playerWalkSpeed;
        this.playerRunMultiplier = playerRunMultiplier;
        this.playerJumpForce = playerJumpForce;
        this.groundCheckDistance = groundCheckDistance;
        this.characterController = characterController;
        this.isGrounded = isGrounded;
        this.gravity = gravity;
        this.velocity = velocity;
        this.verticalRotation = verticalRotation;
        this.rb = rb;
        this.playerGameObject = playerGameObject;
        this.mouseSensitivityX = mouseSensitivityX;
        this.mouseSensitivityY = mouseSensitivityY;
        this.minLookAngleY = minLookAngleY;
        this.maxLookAngleY = maxLookAngleY;
        this.playerCamera = playerCamera;
        this.camPivotRef = camPivotRef;
        this.actualPlayerCamera = actualPlayerCamera;

        this.zTiltAmount = zTiltAmount;
        this.tiltStartSpeed = tiltStartSpeed;
        this.tiltEndSpeed = tiltEndSpeed;
        this.zCurrentTilt = zCurrentTilt;
        this.zTargetTilt = zTargetTilt;
        this.zSmoothTilt = zSmoothTilt;

        this.xTiltAmount = xTiltAmount;
        this.xCurrentTilt = xCurrentTilt;
        this.xTargetTilt = xTargetTilt;
        this.xSmoothTilt = xSmoothTilt;

        this.changeToWorkState = changeToWorkState;
    }

    // Getters for the context variables. These can be used by the different states to access the necessary information about the Player and its environment.

 

}
