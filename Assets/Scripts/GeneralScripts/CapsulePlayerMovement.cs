using UnityEngine;

public class CapsulePlayerMovemen : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }



    void MovePlayer()
    {
        float xValue = Input.GetAxis("Horizontal") * Time.deltaTime;
        float zValue = Input.GetAxis("Vertical") * Time.deltaTime;

        transform.Translate(xValue * moveSpeed, 0f, zValue * moveSpeed);

    }
}
