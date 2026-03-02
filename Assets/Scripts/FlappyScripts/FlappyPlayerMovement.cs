using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class FlappyPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 50f;
    public Rigidbody rb;
    private Vector3 movement;
    public bool northMoving = false;
    public bool southMoving = false;    
    public bool eastMoving = false;
    public bool westMoving = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {


        //Controls for Movement
        if (Input.GetKeyDown(KeyCode.W))
        {
    
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {

        }
        else if (Input.GetKeyDown(KeyCode.S))
        {

        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
      
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
        
        }


    }
}

