using UnityEngine;

public class PlayerStateContext 
{
    [Header("Player Variables")]
    public float playerWalkSpeed = 5f;
    public float playerRunMultiplier = 1.5f;
    public float playerJumpForce = 2f;
    public float groundCheckDistance = 1.5f;
    //Character Controller reference
    public CharacterController characterController;
    public bool isGrounded;
    public float gravity;
    public Vector3 velocity;
    public float verticalRotation = 0f;
    [SerializeField]
    public Rigidbody rb;
    GameObject playerGameObject;

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
    public float zCurrentTilt = 0f;
    public float zTargetTilt = 0f;
    public float zSmoothTilt;

    public float xTiltAmount;
    public float xCurrentTilt = 0f;
    public float xTargetTilt = 0f;
    public float xSmoothTilt;


    public PlayerStateContext(float playerWalkSpeed = 5f, float playerRunMultiplier = 1.5f, float playerJumpForce = 2f, bool isGrounded = true, float verticalRotation = 0f,
    float mouseSensitivityX = 1f, float minLookAngleY = -90f, float zCurrentTilt = 0f, float zTargetTilt = 0f, float xCurrentTilt = 0f, float xTargetTilt = 0f)
    {
        // Initialise the context with nescessary v ariables for the player state machine
        this.playerWalkSpeed = playerWalkSpeed;
        this.playerRunMultiplier = playerRunMultiplier;
        this.playerJumpForce = playerJumpForce;
        this.isGrounded = isGrounded;
        this.verticalRotation = verticalRotation;
        this.mouseSensitivityX = mouseSensitivityX;
        this.minLookAngleY = minLookAngleY;
        this.zCurrentTilt = zCurrentTilt;
        this.zTargetTilt = zTargetTilt;  
        this.xCurrentTilt = xCurrentTilt;
        this.xTargetTilt = xTargetTilt;
    }

    // Getters for the context variables. These can be used by the different states to access the necessary information about the Player and its environment.
    public CharacterController CharacterController => characterController;
    public GameObject PlayerGameObject => playerGameObject;
    public Vector3 Velocity => velocity;
    public float Gravity => gravity;
    public Rigidbody Rb => rb;
    public Transform PlayerCamera => playerCamera;
    public GameObject CamPivotRef => camPivotRef;
    public float ZTiltAmount => zTiltAmount;
    public float TiltStartSpeed => tiltStartSpeed;
    public float TiltEndSpeed => tiltEndSpeed;
    public float ZSmoothTilt => zSmoothTilt;
    public float XTiltAmount => xTiltAmount;
    public float XSmoothTilt => xSmoothTilt;

}
