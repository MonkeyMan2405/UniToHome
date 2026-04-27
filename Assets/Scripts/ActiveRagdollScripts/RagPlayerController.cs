using JetBrains.Annotations;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.Transforms;
using UnityEngine;

public class RagPlayerController : MonoBehaviour
{

    public float speed;
    public float strafeSpeed;

    public float jumpForce;

    public Rigidbody hipsRb;

    public bool isGrounded;
    private bool dontCheckStep;
    private bool leftOrRight;

    private bool rayCheck;
    private bool stepCheck;

    private RaycastHit rayHitInfo;
    private RaycastHit rayFootHitInfo;

    [SerializeField]
    private Transform footDetectorTransform;

    private Vector3 footPosition;
    private Vector3 centerPosition;
    private float pelvisToLastStepDist;
    [SerializeField]
    private float footstepDistance = 2f;

    public GameObject leftLeg;
    public GameObject rightLeg;

    public ConfigurableJoint leftLegJoint;
    public ConfigurableJoint rightLegJoint; 

    public ConfigurableJoint leftForeLeg;
    public ConfigurableJoint rightForeLeg;

    public GameObject leftFoot;
    public GameObject rightFoot;

    public float rotationPower = 10;
    public float footLerpPower = 1f;

    [SerializeField]
    private LayerMask ragdollLayerMask;

    private float distanceToLFoot;
    private float distanceToRFoot;
    private float centerDistance;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //douible check later if this script isn't on the hips, as this would result wrong.
        hipsRb = GetComponent<Rigidbody>();

        isGrounded = true;
        rayCheck = true;
        stepCheck = true;

    }

    private void FixedUpdate()
    {

        if(rayCheck == true)
        {
            CenterOfMassSet();
        }

        if(stepCheck == true)
        {
            BalancerAndSteps();
        }
      
        //if foot hit floor, that is new ref point, need do this


        //Debug.Log(leftOrRight);

        //leftOrRightDetector();

        //LegStepCalculator();

        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                hipsRb.AddForce(hipsRb.transform.right * speed * 1.5f);
            }
            else
            {
                hipsRb.AddForce(hipsRb.transform.right * speed);
            }

            //if (leftOrRight == false)
            //{
            //    leftLeg.transform.RotateAround(leftLeg.transform.position, -Vector3.forward, 90 * rotationPower * Time.deltaTime);
            //    //leftForeLeg.targetRotation = Quaternion.Euler(90, leftForeLeg.targetRotation.x, leftForeLeg.targetRotation.y);

            //    rightForeLeg.targetRotation = Quaternion.Euler(0, rightForeLeg.targetRotation.x, rightForeLeg.targetRotation.y);
            //}

            //if (leftOrRight == true)
            //{
            //    rightLeg.transform.RotateAround(rightLeg.transform.position, -Vector3.forward, 90 * rotationPower * Time.deltaTime);
            //    //rightForeLeg.targetRotation = Quaternion.Euler(-90, rightForeLeg.targetRotation.x, rightForeLeg.targetRotation.y);

            //    leftForeLeg.targetRotation = Quaternion.Euler(0, leftForeLeg.targetRotation.x, leftForeLeg.targetRotation.y);
            //}
        }


        if(Input.GetKeyUp(KeyCode.W))
        {
            //reset foreleg angle
            leftForeLeg.targetRotation = Quaternion.Euler(0, leftForeLeg.targetRotation.x, leftForeLeg.targetRotation.y);
            rightForeLeg.targetRotation = Quaternion.Euler(0, rightForeLeg.targetRotation.x, rightForeLeg.targetRotation.y);

        }





        else if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                hipsRb.AddForce(hipsRb.transform.right * -speed * 1.5f);
            }
            else
            {
                hipsRb.AddForce(hipsRb.transform.right * -speed);
            }

            //if (leftOrRight == false)
            //{
            //    leftLeg.transform.RotateAround(leftLeg.transform.position, Vector3.forward, 90 * rotationPower * Time.deltaTime);
               
            //    rightForeLeg.targetRotation = Quaternion.Euler(0, rightForeLeg.targetRotation.x, rightForeLeg.targetRotation.y);
            //}

            //if (leftOrRight == true)
            //{
            //    rightLeg.transform.RotateAround(rightLeg.transform.position, Vector3.forward, 90 * rotationPower * Time.deltaTime);
                

            //    leftForeLeg.targetRotation = Quaternion.Euler(0, leftForeLeg.targetRotation.x, leftForeLeg.targetRotation.y);
            //}

        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            //reset foreleg angle
            leftForeLeg.targetRotation = Quaternion.Euler(0, leftForeLeg.targetRotation.x, leftForeLeg.targetRotation.y);
            rightForeLeg.targetRotation = Quaternion.Euler(0, rightForeLeg.targetRotation.x, rightForeLeg.targetRotation.y);

        }

        else if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                hipsRb.AddForce(hipsRb.transform.up * -speed * 1.5f);
            }
            else
            {
                hipsRb.AddForce(hipsRb.transform.up * -speed);
            }

        }

        else if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                hipsRb.AddForce(hipsRb.transform.up * speed * 1.5f);
            }
            else
            {
                hipsRb.AddForce(hipsRb.transform.up * speed);
            }

        }




        if (Input.GetKeyDown(KeyCode.Space))
        {

            Ray ragdollRay = new Ray(hipsRb.transform.position, new Vector3(0, -1, 0));

            //5.75 seems right
            if (Physics.Raycast(hipsRb.transform.position, new Vector3(0, -5.75f, 0), out rayHitInfo, 5.75f, ragdollLayerMask))
            {

                Debug.DrawRay(hipsRb.transform.position, new Vector3(0, -5.75f, 0), Color.red);

                if (rayHitInfo.collider.CompareTag("Jumpable"))
                {
                    hipsRb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);


                }
            }
        }


        if (Input.GetKey(KeyCode.T))
        {
            //rightForeLeg.targetRotation = Quaternion.Euler(-90, rightForeLeg.targetRotation.x, rightForeLeg.targetRotation.y);
            rayCheck = true;
        }

        //    if (Input.GetKeyDown(KeyCode.R))
        //    {

            //        Ray footstepRay = new Ray(hipsRb.transform.position, new Vector3(0, -1, 0));


            //        if (Physics.Raycast(hipsRb.transform.position, new Vector3(0, -5.75f, 0), out rayFootHitInfo, 5.75f, ragdollLayerMask))
            //        {
            //            Debug.DrawRay(hipsRb.transform.position, new Vector3(0, -5.75f, 0), Color.red);
            //            footPosition = rayFootHitInfo.point;
            //            Debug.Log(footPosition);
            //        }

            //    }

            //    pelvisToLastStepDist = Vector3.Distance(transform.position, footPosition);


            //    if (pelvisToLastStepDist >= footstepDistance)
            //    {

            //        if (Physics.Raycast(hipsRb.transform.position, new Vector3(0, -5.75f, 0), out rayFootHitInfo, 5.75f, ragdollLayerMask))
            //        {
            //            Debug.DrawRay(hipsRb.transform.position, new Vector3(0, -5.75f, 0), Color.red);
            //            footPosition = rayFootHitInfo.point;
            //            Debug.Log(footPosition);
            //        }

            //        Debug.Log("Take footstep");
            //        TakeStep();

            //    }


            //    if (Input.GetKey(KeyCode.L))
            //    {
            //        //potential backick
            //        leftLeg.transform.RotateAround(leftLeg.transform.position, -Vector3.forward, 90 * rotationPower * Time.deltaTime);
            //    }

            //}

            ////raydown, move and take step,fire another ray, new point, move back leg.
            ////possibly check
            ////make foreleg somehow rotate, either through script or with joint itself


            //public void TakeStep()
            //{
            //    leftLeg.transform.RotateAround(leftLeg.transform.position, -Vector3.forward, 90 * rotationPower * Time.deltaTime);
            //}
    }


    public void CenterOfMassSet()
    {

        if (Physics.Raycast(hipsRb.transform.position, new Vector3(0, -1, 0), out rayFootHitInfo, 5.75f, ragdollLayerMask))
        {
            centerPosition = rayHitInfo.point = new Vector3(0, hipsRb.transform.position.y, 0);
            rayCheck = false;
            Debug.Log(centerPosition);
            //maybe set back to true when stopping movement?
        }
    }


   public void BalancerAndSteps()
   {
        centerDistance = Vector3.Distance(centerPosition, hipsRb.transform.position);

        if (centerDistance > footstepDistance)
        {
            stepCheck = false;
            rayCheck = true;
            Debug.Log("take step");
            StartCoroutine(TakeStep());       
        }

   }


    IEnumerator TakeStep()
    {
        if (leftOrRight == false)
        {
            //leftForeLeg.transform.position = 
            leftLegJoint.targetRotation = Quaternion.Euler(-40, leftLegJoint.targetRotation.x, leftLegJoint.targetRotation.y);
            //leftForeLeg.targetRotation = Quaternion.Euler(-45, leftForeLeg.targetRotation.x, leftForeLeg.targetRotation.y);
            yield return new WaitForSeconds(0.4f);

            if (Physics.Raycast(hipsRb.transform.position, new Vector3(0, -1, 0), out rayFootHitInfo, 5.75f, ragdollLayerMask))
            {
                centerPosition = rayHitInfo.point;

                leftLegJoint.targetRotation = Quaternion.Euler(0, leftLegJoint.targetRotation.x, leftLegJoint.targetRotation.y);
                leftFoot.transform.position = Vector3.Lerp(leftFoot.transform.position, centerPosition, footLerpPower * Time.deltaTime);

                stepCheck = true;
                leftOrRight = true;
            }
        }

        else
        {
            //leftForeLeg.transform.position = 
            rightLegJoint.targetRotation = Quaternion.Euler(40, rightLegJoint.targetRotation.x, rightLegJoint.targetRotation.y);
            //leftForeLeg.targetRotation = Quaternion.Euler(-45, leftForeLeg.targetRotation.x, leftForeLeg.targetRotation.y);
            yield return new WaitForSeconds(0.4f);

            if (Physics.Raycast(hipsRb.transform.position, new Vector3(0, -1, 0), out rayFootHitInfo, 5.75f, ragdollLayerMask))
            {
                centerPosition = rayHitInfo.point;

                rightLegJoint.targetRotation = Quaternion.Euler(0, rightLegJoint.targetRotation.x, rightLegJoint.targetRotation.y);
                rightFoot.transform.position = Vector3.Lerp(rightFoot.transform.position, centerPosition, footLerpPower * Time.deltaTime);

                stepCheck = true;
                leftOrRight = false;
            }
        }

            StopCoroutine(TakeStep());
    }








    public void LegStepCalculator()
    {
        Ray footstepRay = new Ray(hipsRb.transform.position, new Vector3(0, -1, 0));

        if (Physics.Raycast(hipsRb.transform.position, new Vector3(0, -5.75f, 0), out rayFootHitInfo, 5.75f, ragdollLayerMask))
        {

            Debug.DrawRay(hipsRb.transform.position, new Vector3(0, -5.75f, 0), Color.red);

            distanceToLFoot = Vector3.Distance(rayFootHitInfo.point, leftFoot.transform.position);
            distanceToRFoot = Vector3.Distance(rayFootHitInfo.point, rightFoot.transform.position);

            if (distanceToLFoot > footstepDistance && distanceToRFoot > distanceToLFoot)
            {
   
                leftFoot.transform.position = Vector3.Lerp(leftFoot.transform.position, rayFootHitInfo.point + new Vector3(0, 8, 0), footLerpPower * Time.deltaTime);
                leftOrRight = false;
            }

            if (distanceToRFoot > footstepDistance && distanceToLFoot > distanceToRFoot)
            {

                rightFoot.transform.position = Vector3.Lerp(rightFoot.transform.position, rayFootHitInfo.point + new Vector3(0,8,0), footLerpPower * Time.deltaTime);
                leftOrRight = true;
            }
        }

        //if (Physics.Raycast(leftFoot.transform.position, new Vector3(0, -0.1f, 0), out rayFootHitInfo, 0.4f, ragdollLayerMask))
        //{
        //    leftOrRight = true;
        //    Debug.Log("Hi");
        //}
        //else if (Physics.Raycast(rightFoot.transform.position, new Vector3(0, -0.1f, 0), out rayFootHitInfo, 0.4f, ragdollLayerMask))
        //{
        //    leftOrRight = false;
        //    Debug.Log("Ho");
        //}

    }

   
    //public void leftOrRightDetector()
    //{

    //    if (dontCheckStep == false)
    //    {
    //        if (leftOrRight == false)
    //        {
    //            if (Physics.Raycast(leftFoot.transform.position, new Vector3(0, -1f, 0), out rayFootHitInfo, 1f, ragdollLayerMask))
    //            {
    //                Debug.DrawRay(leftFoot.transform.position, new Vector3(0, 1f, 0), Color.red);

    //                //current foot vector 3 = where raycast hits, need to only check once
    //                footPosition = rayFootHitInfo.point;
    //                Debug.Log(footPosition);

    //                dontCheckStep = true;
    //            }
    //        }
    //        else
    //        {
    //            if (Physics.Raycast(rightFoot.transform.position, new Vector3(0, -0.5f, 0), out rayFootHitInfo, 0.5f, ragdollLayerMask))
    //            {
    //                Debug.DrawRay(rightFoot.transform.position, new Vector3(0, 0.5f, 0), Color.red);

    //                //current foot vector 3 = where raycast hits, need to only check once
    //                footPosition = rayFootHitInfo.point;
    //                Debug.Log(footPosition);

    //                dontCheckStep = true;
    //            }

    //        }

    //    }


    ////set dist to check as dist between position and old foot position grteater than step distance, then take step
    //pelvisToLastStepDist = Vector3.Distance(footDetectorTransform.position, footPosition);

    //if (pelvisToLastStepDist <= footstepDistance)
    //{
    //    dontCheckStep = false; // cast ray again so set new foot position, then switch leg
    //    SwitchStep();

    //}



    public void SwitchStep()
    {

        if(leftOrRight == false)
        {
            leftOrRight = true;
        }
        else
        {
            leftOrRight = false;
        }

    }

    public void footstepDetector()
    {
        if (dontCheckStep == false)
        {
            Ray footstepRay = new Ray(hipsRb.transform.position, new Vector3(0, -1, 0));

            if (Physics.Raycast(hipsRb.transform.position, new Vector3(0, -5.75f, 0), out rayFootHitInfo, 5.75f, ragdollLayerMask))
            {
                Debug.DrawRay(hipsRb.transform.position, new Vector3(0, -5.75f, 0), Color.red);

                //current foot vector 3 = where raycast hits, need to only check once
                footPosition = rayFootHitInfo.point;
                Debug.Log(footPosition);

                //dontCheckStep = true;
            }

        }

        //set dist to check as dist between position and old foot position grteater than step distance, then take step
        pelvisToLastStepDist = Vector3.Distance(footDetectorTransform.position, footPosition);


        if (pelvisToLastStepDist >= footstepDistance)
        {
            TakeStep();

        }
            
           
 

    }

    //public void TakeStep()
    //{
    //    leftLeg.transform.RotateAround(leftLeg.transform.position, -Vector3.forward, 90 * rotationPower * Time.deltaTime);
    //    if (leftLeg.transform.rotation.y >= -45)
    //    {
    //        Debug.Log("Should maybe work lol");
    //    }
    //}

    //move runs the take step command,
    //a seperate script just alternates what leg it runs on?

}
