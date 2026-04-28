using System.Collections;
using UnityEngine;

public class RagController2 : MonoBehaviour
{
    [SerializeField]
    private LayerMask floorLM;
    [SerializeField]
    private float stepDistance;
    [SerializeField]
    private float forceMultiplier;

    public Vector3 rayheightincrease;
    public Rigidbody hipsRb;
    public Transform leftFoot;
  
    public Rigidbody leftFootRB;
    public Rigidbody rightFootRB;
    public Vector3 pelvisToFloorPos;

    private Vector3 oldPos;
    private Vector3 newPos;

    private bool checkRay1;
    private bool checkRay2;
    private bool checkCoroutine;
    private bool takeTheStep;
    private bool leftOrRight;

    [SerializeField]
    private float stepHeight;
    [SerializeField]
    private float stepTime;

    private float stepTimer;
    private float stepTimer2;


    [SerializeField]
    private float rotationPower;

    private RaycastHit hitInfo;
    private RaycastHit hitInfo2;

    [SerializeField]
    private ConfigurableJoint leftLegJoint;

    [SerializeField]
    private ConfigurableJoint leftForelegJoint;

    [SerializeField]
    private ConfigurableJoint rightLegJoint;

    [SerializeField]
    private ConfigurableJoint rightForelegJoint;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       checkRay1 = true;
       leftOrRight = false;

    }

    // Update is called once per frame
    void Update()
    {
     
        //hipsRb.AddForce(Vector3.down * forceMultiplier);

        Checkers();

      


        if (Input.GetKey(KeyCode.W))
        {
            hipsRb.AddForce(Vector3.right * forceMultiplier);
        }
        else
        {
            //hipsRb.linearVelocity = Vector3.Lerp(hipsRb.linearVelocity, Vector3.zero, 0.75f);
        }

        if (Input.GetKey(KeyCode.S))
        {
            //leftForelegJoint.targetRotation = Quaternion.Lerp(leftForelegJoint.targetRotation, Quaternion.Euler(60, 0, 0), rotationPower * Time.deltaTime);
        }
        else
        {
            //leftForelegJoint.targetRotation = Quaternion.Lerp(leftForelegJoint.targetRotation, Quaternion.Euler(0, 0, 0), rotationPower * Time.deltaTime);
        }
       









    }


    public void Checkers()
    {
        if (checkRay1 == true)
        {
            RayCheck1();
        }
        else
        {
            RayCheckDist();
        }



        if (takeTheStep == true)
        {
            TakeStep();
        }
        

    }




    public void RayCheck1()
    {
        //Debug.DrawRay(hipsRb.transform.position, new Vector3(0, -6, 0), Color.red);

        if (Physics.Raycast(hipsRb.transform.position, new Vector3(0, -6, 0), out hitInfo, floorLM))
        {
            checkRay1 = false;
            oldPos = hitInfo.point;
            Debug.Log("Hit");
        }

    }

    public void RayCheckDist()
    {
        if (Physics.Raycast(hipsRb.transform.position, new Vector3(0, -6, 0), out hitInfo2, floorLM))
        {

           newPos = hitInfo2.point;

            if(Vector3.Distance(newPos, oldPos) > stepDistance)
            {
               
               Debug.Log("Step");  
               takeTheStep = true;

            }


        }




    }

    public void TakeStep()
    {

        stepTimer += Time.deltaTime;

        //left
        if (leftOrRight == false)
        {
            if (stepTimer < stepTime)
            {
                leftLegJoint.targetRotation = Quaternion.Lerp(  leftLegJoint.targetRotation, Quaternion.Euler(-55, 0, 0), rotationPower * Time.deltaTime);
                leftForelegJoint.targetRotation = Quaternion.Lerp(leftForelegJoint.targetRotation, Quaternion.Euler(55, 0, 0), rotationPower * Time.deltaTime);
                //leftFootRB.AddForce(Vector3.up * stepHeight, ForceMode.Impulse);
            }
            else
            {
                stepTimer2 += Time.deltaTime;
                if (stepTimer2 < stepTime % 1.25)
                {
                    leftLegJoint.targetRotation = Quaternion.Lerp(leftLegJoint.targetRotation, Quaternion.Euler(0, 0, 0), rotationPower * Time.deltaTime);
                    leftForelegJoint.targetRotation = Quaternion.Lerp(leftForelegJoint.targetRotation, Quaternion.Euler(0, 0, 0), rotationPower * Time.deltaTime);

                    //hipsRb.position = Vector3.Lerp(hipsRb.position, new Vector3(leftFootRB.position.x, hipsRb.position.y, hipsRb.position.z), rotationPower * Time.deltaTime);
                }
                else
                {
                    stepTimer = 0;
                    stepTimer2 = 0;
                    takeTheStep = false;
                    leftOrRight = true;
                    checkRay1 = true;
                }
            }
        }

        //right
        else
        {

            if (stepTimer < stepTime)
            {
                rightLegJoint.targetRotation = Quaternion.Lerp(rightLegJoint.targetRotation, Quaternion.Euler(55, 0, 0), rotationPower * Time.deltaTime);
                rightForelegJoint.targetRotation = Quaternion.Lerp(rightForelegJoint.targetRotation, Quaternion.Euler(-55, 0, 0), rotationPower * Time.deltaTime);
            }
            else
            {
                stepTimer2 += Time.deltaTime;
                if (stepTimer2 < stepTime % 1.25)
                {
                    rightLegJoint.targetRotation = Quaternion.Lerp(rightLegJoint.targetRotation, Quaternion.Euler(0, 0, 0), rotationPower * Time.deltaTime);
                    rightForelegJoint.targetRotation = Quaternion.Lerp(rightForelegJoint.targetRotation, Quaternion.Euler(0, 0, 0), rotationPower * Time.deltaTime);

                    //hipsRb.position = Vector3.Lerp(hipsRb.position, new Vector3(rightFootRB.position.x, hipsRb.position.y, hipsRb.position.z), rotationPower * Time.deltaTime);
                }
                else
                {
                    stepTimer = 0;
                    stepTimer2 = 0;
                    takeTheStep = false;
                    leftOrRight = false;
                    checkRay1 = true;
                }
            }
        }
    }

    

    public void WhichLegCalculator()
    {

    }


}
