using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEditor.PackageManager;
using UnityEngine;

public class ChaseState : EnemyState
{
    private RaycastHit hitInfo;
    private float lostSightTimer;
    private bool lostSight;


    private Material originalFaceMaterial;

    private float rayStartAngle;
    private float rayAngle;
    private Quaternion rayRotation;
    private Vector3 rayDirection;

    public ChaseState(EnemyStateContext context, EnemyStateMachine.EEnemyState state) : base(context, state)
    {
        EnemyStateContext Context = context;
    }



    public override void EnterState()
    {
        Debug.Log("Entered Chase State");

        //Set og face material to what it was
        originalFaceMaterial = Context.enemyMeshRenderer.material;
        //change face material to glowing red
        Context.enemyMeshRenderer.material = Context.enemyFaceMaterial;

        lostSightTimer = 0;
        lostSight = false;

    }



    public override void UpdateState()
    {
        Context.raySpacing = 12f;
        Context.rayCount = 12;

        Debug.Log("Sight Timer: " + lostSightTimer);

        Ray enemyRay = new Ray(Context.EnemyRb.transform.position, Context.EnemyRb.transform.forward);

        //A timer is started when loss sight of player. Grace period applied where will keep searching. once over, will transition to next state
        if (lostSightTimer <= 5f)
        {
           WideRayCone();
        }
        else
        {
            lostSight = true;
        }   
        
    }
            



    public override void ExitState()
    {
        //switch back to normal face material
        Context.enemyMeshRenderer.material = originalFaceMaterial;
    }



    public override void OnTriggerEnter(Collider other)
    {

    }



    public override void OnTriggerExit(Collider other)
    {

    }



    public override void OnTriggerStay(Collider other)
    {

    }



    public override EnemyStateMachine.EEnemyState GetNextState()
    {

        if (lostSight == true)
        {
            return EnemyStateMachine.EEnemyState.SearchPoi;
        }
        return StateKey;
    }



    public void ChaseThePlayer()
    {
        Context.EnemyAgent.SetDestination(Context.TargetTransform.position);
    }



    public void WideRayCone()
    {
        Ray enemyRay = new Ray(Context.EnemyRb.transform.position, Context.EnemyRb.transform.forward);

     for (int i = 0; i < Context.rayCount; i++)
        {
            //Calculate angle for the rays
            float rayStartAngle = -(Context.raySpacing * Context.rayCount - 1) / 2f;
            float rayAngle = rayStartAngle + (i * Context.raySpacing);
            rayRotation = Quaternion.Euler(0, rayAngle, 0);
            rayDirection = rayRotation * Context.EnemyRb.transform.forward;
            

            //Perform the raycast. Red and blocked if hitting an object, green if not.

            if (Physics.Raycast(Context.EnemyRb.transform.position, rayDirection, out hitInfo, Context.rayCheckDistance))
            {
                Debug.DrawRay(Context.EnemyRb.transform.position, rayDirection * hitInfo.distance, Color.green);
                
                //Check if hit the player, chase and reset timer
                if (hitInfo.collider.CompareTag("Player"))
                {
                    ChaseThePlayer();
                    lostSightTimer = 0;
                    Debug.DrawRay(Context.EnemyRb.transform.position, rayDirection * hitInfo.distance, Color.red);
                }
                
                //If not hit player, decrease timer
                //Checsks specifically forward ray
                if (Physics.Raycast(Context.EnemyRb.transform.position, Context.TargetTransform.forward, out hitInfo, Context.rayCheckDistance))
                {
                    if (!hitInfo.collider.CompareTag("Player"))
                    {
                        lostSightTimer += Time.deltaTime;
                    }
                }
            }
            //If not hit anything, decrease timer
            else
            {
                lostSightTimer += Time.deltaTime;
            }        
        }
    }


     // public void SingleRaySearch()
    // {
    //      Debug.DrawRay(Context.EnemyRb.transform.position, Context.targetTransform.position * Context.rayCheckDistance, Color.green);

    //     if (Physics.Raycast(Context.EnemyRb.transform.position, Context.EnemyRb.transform.forward, out hitInfo, Context.rayCheckDistance))
    //     {
    //         //Check if hit the player, chase and reset timer
    //         if (hitInfo.collider.CompareTag("Player"))
    //         {
    //             Context.EnemyAgent.SetDestination(Context.targetTransform.position);
    //             lostSightTimer = 0;
    //         }
    //         //If not hit player, decrease timer
    //         else
    //         {
    //             lostSightTimer += Time.deltaTime;
    //         }
    //     }
    //     //If not hit anything, decrease timer
    //     else
    //     {
    //         lostSightTimer += Time.deltaTime;
    //     }
       
    // }


}



    
      
        
        

