
using UnityEngine;
using UnityEngine.Rendering;

public class SilverPlayerMovement: MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float PlayerSpeed = 50000.0f;
    private Rigidbody rb;
    public float MouseSensitivity = 2.0f;
    //private float VerticalRotation = 0.0f;
    public Transform PlayerLocation;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //get left and right mouse movement, scale by MouseSensitivty.
        float HorizontalRotation = Input.GetAxis("Mouse X") * MouseSensitivity;

        //apply movement to player's rotation around the Y axis.
        transform.Rotate(0, HorizontalRotation, 0);

        //manipulate vertical rotation using mouse y, scale by MouseSensitivity.
        float VerticalRotation = Input.GetAxis("Mouse Y") * MouseSensitivity;

        //clamp rotation values so camera cannot clip through floor.
        VerticalRotation = Mathf.Clamp(VerticalRotation, -90, 90);

        //adjust camera Vertical Rotation using Quaternion.
        Camera.main.transform.localRotation = Quaternion.Euler(VerticalRotation, 0, 0);

        //mapping left-right, up-down to horizontal and vertical.
        float MoveHorizontal = Input.GetAxis("Horizontal");
        float MoveVertical = Input.GetAxis("Vertical");

        // plug in axis values, and apply PlayerSpeed scalar and time modifier.
        Vector3 Movement = new Vector3(MoveHorizontal, 0, MoveVertical) * PlayerSpeed * Time.deltaTime;

        //ensure movement vector points in the correct direction.


        //apply movement vector to rigidbody/
        rb.linearVelocity = new Vector3(Movement.x, rb.linearVelocity.y, Movement.z);


        //player jump



    }
}

