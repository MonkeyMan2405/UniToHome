using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

public class PatrolState : EnemyState
{

    [SerializeField]
    private float closingRange = 1f;
    private bool doNotCheck = false;
    private Transform currentPatrolTransform;
    RaycastHit hitInfo;
    private bool seenPlayer = false;

    private float rayStartAngle;
    private float rayAngle;
    private Quaternion rayRotation;
    private Vector3 rayDirection;


    public PatrolState(EnemyStateContext context, EnemyStateMachine.EEnemyState state) : base(context, state)
    {
        EnemyStateContext Context = context;
    }



    public override void EnterState()
    {
        Debug.Log("Entered Patrol State");
        //Set the patrol points list
        SetNewTargetAndMove();
        seenPlayer = false;
    }



    public override void UpdateState()
    {
        //Check if reached target and prevent repeats with Dnc bool. if so, set new target
        if (Context.EnemyAgent.remainingDistance <= closingRange && doNotCheck == false)
        {
            Debug.Log("Reached");
            doNotCheck = true;
            SetNewTargetAndMove();
        }

        //Raycast Cone
        Context.raySpacing = 16f;
        Context.rayCount = 12;

        Ray enemyRay = new Ray(Context.EnemyRb.transform.position, Context.EnemyRb.transform.forward);

        for (int i = 0; i < Context.rayCount; i++)
        {
            float rayStartAngle = -(Context.raySpacing * Context.rayCount - 1) / 2f;
            //Calculate angle for the rays
            float rayAngle = rayStartAngle + (i * Context.raySpacing);
            rayRotation = Quaternion.Euler(0, rayAngle, 0);
            rayDirection = rayRotation * Context.EnemyRb.transform.forward;

            // Perform the raycast. Red and blocked if hitting an object, green and passing through if not.

            Debug.DrawRay(Context.EnemyRb.transform.position, rayDirection * Context.rayCheckDistance, Color.green);
            if (Physics.Raycast(Context.EnemyRb.transform.position, rayDirection, out hitInfo, Context.rayCheckDistance))
            {
                //check if the raycast hit the player
                Debug.DrawRay(Context.EnemyRb.transform.position, rayDirection * hitInfo.distance, Color.red);
                if (hitInfo.collider.CompareTag("Player"))
                {
                    seenPlayer = true;
                }
            }
        }


        //temp
        if (Input.GetKeyDown(KeyCode.Space))
        {
            seenPlayer = true;
        }
    }


    public override void ExitState()
    {
        //Is called when exiting state, stop heading to current target
        StopPursuingCurrentTarget();
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
        if (seenPlayer == true)
        {
            return EnemyStateMachine.EEnemyState.Chase;
        }
        return StateKey;
    }



    public void SetNewTargetAndMove()
    {
        //Target is random from 0 to size of list
        currentPatrolTransform = Context.patrolPointsList[Random.Range(0, Context.patrolPointsList.Count)];
        Debug.Log("Current Target is " + currentPatrolTransform.name);
        Context.EnemyAgent.SetDestination(currentPatrolTransform.position);
        doNotCheck = false;
    }


    public void StopPursuingCurrentTarget()
    {
       Context.EnemyAgent.SetDestination(Context.EnemyAgent.transform.position);
    }

     


    //Implement Raycast system,
    //Somehow stop agent from moving mid player detect +
    //Switch to Chase State logic +




}

   