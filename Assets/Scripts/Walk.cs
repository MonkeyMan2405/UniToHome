using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    public GameObject com;
    public Transform cam;
    public Rigidbody hips;
    public float speed = 7;
    // Start is called before the first frame update
    void Start()
    {
        //com = GameObject.Find("person/joint");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!com) com = GameObject.Find("person/joint");
        if (Input.GetKey(KeyCode.W))
        {
            com.transform.rotation = Quaternion.LookRotation(-cam.forward);
        }
        if (Input.GetKey(KeyCode.A))
        {
            com.transform.rotation = Quaternion.LookRotation(cam.right);
        }
        if (Input.GetKey(KeyCode.S))
        {
            com.transform.rotation = Quaternion.LookRotation(cam.forward);
        }
        if (Input.GetKey(KeyCode.D))
        {
            com.transform.rotation = Quaternion.LookRotation(-cam.right);
        }
        com.transform.eulerAngles = new Vector3 (0, com.transform.eulerAngles.y, 0);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            hips.AddForce(hips.transform.forward * speed, ForceMode.Acceleration);
        }
    }
}
