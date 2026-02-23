using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;

    //variables for changing position and sensitivity.
    public float Distance = 5.0f;
    public float Sensitivity = 2.0f;
    public float HeightOffset = 1.5f;

    //variables for changing rotation.
    public float RotationX = 0.0f;
    public float RotationY = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //grab input from mouse, scale by sensitivity.
        float MouseX = Input.GetAxis("Mouse X") * Sensitivity;
        float MouseY = Input.GetAxis("Mouse Y") * Sensitivity;

        //rotate on opposite axis, because of how rotation works Around an Axis. Up and Down is reversed to feel natural.
        RotationY += MouseX;
        RotationX -= MouseY;

        //prevent camera from inverting
        RotationX = Mathf.Clamp(RotationX, -90, 90);

        //apply rotation to camera's transform.
        transform.localRotation = Quaternion.Euler(RotationX, RotationY, 0);

        //point in direction of player
        transform.localRotation = Quaternion.Euler(RotationX, 0, 0);
    }

    private void LateUpdate()
    {
        //set up offset as a vector, as a function of the target's position, direction and height.
      
        Vector3 TargetOffset = target.position - Vector3.forward * Distance + Vector3.up * HeightOffset;


        //set camera position to the offset we just calculated.
        transform.position = TargetOffset;

        //keep camera looking at the target.
      transform.LookAt(target);
    }
}
