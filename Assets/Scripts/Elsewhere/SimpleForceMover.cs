using UnityEngine;

public class SimpleForceMover : MonoBehaviour
{
    public Rigidbody hipsRb;
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            hipsRb.AddForce(hipsRb.transform.right * speed);

        }
    }
}
