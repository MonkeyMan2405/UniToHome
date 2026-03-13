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


    public PatrolState(EnemyStateContext context, EnemyStateMachine.EEnemyState state) : base(context, state)
    {
        EnemyStateContext Context = context;
    }



    public override void EnterState()
    {
        Debug.Log("Entered Patrol State");
        //Set the patrol points list
        SetNewTargetAndMove();
    }



    public override void UpdateState()
    {
        //Check if reached target and prevent repeats with Dnc bool. if so, set new target
        if (Context.EnemyAgent.remainingDistance <= closingRange && doNotCheck == false)
        {
            Debug.Log("Reached");
            //doNotCheck = true;
            SetNewTargetAndMove();

        }


        if (Physics.Raycast(Context.EnemyRb.transform.position, Context.EnemyRb.transform.forward,out RaycastHit hitInfo, Context.rayCheckDistance, Context.layerMask))
        {
            Debug.Log("Rayhit");
            Debug.DrawRay(Context.EnemyRb.transform.position, Context.EnemyRb.transform.forward * hitInfo.distance, Color.green);
        }
        else
        {
            Debug.Log("Ray no hit");
            Debug.DrawRay(Context.EnemyRb.transform.position, Context.EnemyRb.transform.forward * Context.rayCheckDistance, Color.blue);
        }

        //Temp
        if (Input.GetKey(KeyCode.Space))
        {
            StopPursuingCurrentTarget();
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
        if (Input.GetKeyDown(KeyCode.Space))
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
    }




    public void StopPursuingCurrentTarget()
    {
       Context.EnemyAgent.SetDestination(Context.EnemyAgent.transform.position);
    }


    //Implement Raycast system,
    //Somehow stop agent from moving mid player detect +
    //Switch to Chase State logic +

  


}

   