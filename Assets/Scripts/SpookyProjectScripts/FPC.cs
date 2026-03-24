using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class FPC : MonoBehaviour
{
    [Header("Player Variables")]
    public float playerWalkSpeed = 5f;
    public float playerRunMultiplier = 1.5f;
    public float playerJumpForce = 2f;
    public float groundCheckDistance = 1.5f;
    //Character Controller reference
    private CharacterController characterController;
    public bool isGrounded;
    public float gravity;
    private Vector3 velocity;
    private float verticalRotation = 0f;
    [SerializeField]
    private Rigidbody rb;

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

    public int exInt;



    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        //locks when clicking on screen
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        CameraSetup();
        CameraTilt();
        ForwardAndBackwardTilt();
        Movement();

    }


    private void CameraTilt()
    {
        bool leftStrafe = Input.GetKey(KeyCode.A);
        bool rightStrafe = Input.GetKey(KeyCode.D);
        bool forwardStrafe = Input.GetKey(KeyCode.W);
        bool backwardStrafe = Input.GetKey(KeyCode.S);

        //Left-Right Strafing Tilting
        // Determine the Ztarget tilt based on strafing input. If strafing left, set target tilt to positive value. If strafing right, set to negative value. If not strafing, set to zero
        if (leftStrafe && !rightStrafe)
        {
            zTargetTilt = zTiltAmount;
        }
        else if (rightStrafe && !leftStrafe)
        {
            zTargetTilt = -zTiltAmount;
        }
        else
        {
            zTargetTilt = 0f;
        }


        // Smoothly interpolate current tilt to target tilt. If target tilt zero, use tilt end speed forquicker return to neutral. else, use tilt start speed for slower transition when starting to strafe.
        if (zTargetTilt == 0)
        {
            zSmoothTilt = tiltEndSpeed;
        }
        else
        {
            zSmoothTilt = tiltStartSpeed;
        }

        // Lerp the current tilt towards the target tilt using the determined smooth speed and delta time
        zCurrentTilt = Mathf.Lerp(zCurrentTilt, zTargetTilt, zSmoothTilt * Time.deltaTime);



        // Determine the Xtarget tilt based on strafing input. If strafing forward, set target tilt to positive value. If strafing backwards, set to negative value. If not strafing, set to zero
        if (forwardStrafe && !backwardStrafe)
        {
            xTargetTilt = xTiltAmount;
        }
        else if (backwardStrafe && !forwardStrafe)
        {
            xTargetTilt = -xTiltAmount;
        }
        else
        {
            xTargetTilt = 0f;
        }


        // Smoothly interpolate current tilt to target tilt. If target tilt zero, use tilt end speed for quicker return to neutral. else, use tilt start speed for slower transition when starting to strafe.
        if (xTargetTilt == 0)
        {
            xSmoothTilt = tiltEndSpeed;
        }
        else
        {
            xSmoothTilt = tiltStartSpeed;
        }

        // Lerp the current tilt towards the target tilt using the determined smooth speed and delta time
        xCurrentTilt = Mathf.Lerp(xCurrentTilt, xTargetTilt, xSmoothTilt * Time.deltaTime);
    }
    

    public void ForwardAndBackwardTilt()
    {
        camPivotRef.transform.localRotation = Quaternion.Euler(xCurrentTilt, 0, 0);
    }


    public void Movement()
    {
        //Maps WASD to horizontal and vertical movement
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");


        //Movement
        Vector3 moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
        //Prevents faster diagonal movement
        moveDirection.Normalize();

        float speed = playerWalkSpeed;
        if (Input.GetAxis("Sprint") > 0)
        {
            speed *= playerRunMultiplier;
        }


        //Something
        characterController.Move(moveDirection * speed * Time.deltaTime);
        //Gravity
        characterController.Move(Vector3.down * gravity * Time.deltaTime);

        //Move the character controller using inbuilt function
        characterController.Move(velocity * Time.deltaTime);
    }


    public void CameraSetup()
    {
        //Camera set up
        if (playerCamera != null)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY;

            verticalRotation -= mouseY;
            //Clamp the vertical rotation to prevent flipping. Clamps A by given floats
            verticalRotation = Mathf.Clamp(verticalRotation, minLookAngleY, maxLookAngleY);

            // Rotate the player horizontally and with tilt
            playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0, zCurrentTilt);
            transform.Rotate(Vector3.up * mouseX);
            //transform.localRotation = Quaternion.Euler(mouseX, mouseY, currentTilt);
        }
    }


    //if (Input.GetButtonDown("Jump"))
        //{
        //    RaycastHit hit;
        //    if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance))
        //    {
        //       rb.AddForce(Vector3.up * playerJumpForce, ForceMode.Impulse);
        //    }
        //    else
        //    {
        //        velocity.y += gravity * Time.deltaTime;
        //    }
        //}
    
}

