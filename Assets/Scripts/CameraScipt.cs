using UnityEngine;

public class CameraScipt : MonoBehaviour
{
    [SerializeField] private float MouseSensitivity = 10f;
    private float rotationX;
    private float rotationY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float xMouse = Input.GetAxis("Horizontal") * MouseSensitivity;
        float yMouse = Input.GetAxis("Vertical") * MouseSensitivity;

        rotationY += xMouse;
        rotationX -= yMouse;

        rotationX = Mathf.Clamp(rotationX, -90, 90);

        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
    }
}
