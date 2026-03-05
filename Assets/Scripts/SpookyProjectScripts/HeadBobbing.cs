using System.Runtime.CompilerServices;
using UnityEngine;

public class HeadBobbing : MonoBehaviour
{

    public float amount = 0.005f; // The amount of head bobbing
    public float frequency = 10f; // The frequency of head bobbing 
    public float smoothness = 5f; // The smoothness of head bobbing
    Vector3 startPos;
    public float yUpAndDownMultiplier = 1.5f; // Multiplier for the vertical head bobbing effect
    public float xLeftAndRightMultiplier = 1.6f; // Multiplier for the horizontal head bobbing effect
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.localPosition; // Store the initial position of the camera
    }

    // Update is called once per frame
    void Update()
    {
        CheckForHeadBobTrigger();
        StopHeadBobbing();


        if (Input.GetButtonDown("Sprint"))
        {
            frequency *= 2f; // Increase frequency for faster head bobbing when sprinting
            smoothness *= 2f; // Increase smoothness for faster head bobbing when sprinting
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            frequency /= 2f; // Reset frequency when sprinting stops
            smoothness /= 2f; // Reset smoothness when sprinting stops
        }
    }

    private void CheckForHeadBobTrigger()
    {
        //check if player is moving by checking the magnitude of the input vector
        float inputMagnitude = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;
        if (inputMagnitude > 0f)
        {
            // Trigger head bobbing animation
            // Example: transform.localPosition = new Vector3(0, Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmount, 0);
            StartHeadBobbing();
        }
    }

        private Vector3 StartHeadBobbing()
        {
            Vector3 pos = Vector3.zero;
        // Calculate the head bobbing effect using sine and cosine + apply amount and frequency to create bobbing motion. Vertical bobbing influenced by yUpAndDownMultiplier, horizontal bobbing influenced by xLeftAndRightMultiplier.
        pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * frequency) * amount * yUpAndDownMultiplier, smoothness * Time.deltaTime);
            pos.x += Mathf.Lerp(pos.x, Mathf.Cos(Time.time * frequency / 2f) * amount * 1.6f, smoothness * Time.deltaTime);
            // Apply the head bobbing effect to the camera's local position
            transform.localPosition += pos;

            return pos;
        }

    private void StopHeadBobbing()
    {
        if (transform.localPosition == startPos) return;
        transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, 1 * Time.deltaTime);
    }

}
