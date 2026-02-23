using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 5.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float HorizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");

        Vector3 MovementDirection = new Vector3 (HorizontalInput, 0f, VerticalInput);

        transform.Translate(MovementDirection * MoveSpeed * Time.deltaTime);
        
    }

    public void Jump()
    {

    }
}
