using UnityEngine;

public class FPC : MonoBehaviour
{
    public float playerWalkSpeed = 5f;
    public float playerRunMultiplier = 1.5f;
    public float playerJumpForce = 2f;
    public float groundCheckDistance = 1.5f;
    public float mouseSensitivityX = 1f;
    public float mouseSensitivityY = 1f;
    public float minLookAngleY = -90f;
    public float maxLookAngleY = 90f;
    [SerializeField]
    private Rigidbody rb;

    //Transform for Camera
    public Transform playerCamera;

    //Character Controller reference
    private CharacterController characterController;
    public bool isGrounded;

    public float gravity = -9.81f;
    private Vector3 velocity;
    private float verticalRotation = 0;

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
        //Maps WASD to horizontal and vertical movement
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");



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


        //Movement
        Vector3 moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
        //Prevents faster diagonal movement
        moveDirection.Normalize();


        float speed = playerWalkSpeed;
        if(Input.GetAxis("Sprint") > 0)
        {
            speed *= playerRunMultiplier;
        }

        //Something
        characterController.Move(moveDirection * speed * Time.deltaTime);

        //Move the character controller
        characterController.Move(velocity * Time.deltaTime);


        //Camera set up.
        if(playerCamera != null)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY;

            verticalRotation -= mouseY;
            //Clamp the vertical rotation to prevent flipping. Clamps A by given floats
            verticalRotation = Mathf.Clamp(verticalRotation, minLookAngleY, maxLookAngleY);

            // Rotate the player horizontally
            playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
            transform.Rotate(Vector3.up * mouseX);
        }
    }
}
