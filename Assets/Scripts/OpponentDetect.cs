using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class OpponentDetect : MonoBehaviour
{

    private bool hasStartedChasingPlayer;
    private NavMeshAgent OpponentAgent;
    public Transform CurrentGoldTarget;
    public List<Transform> GoldTargetsList;
    private int GoldIndex;
    private bool SettingNewTarget;
    private EnemyStates EnemyState;
    public float EnemyRange = 0.1f;
    public int EnemyWaitTime = 5;
    private bool wasChasing;
    public Vector3 playerLastLocation;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OpponentAgent = GetComponent<NavMeshAgent>();
        EnemyState = EnemyStates.Patroling;
        GoldIndex = 0;
        CurrentGoldTarget = GoldTargetsList[GoldIndex];
        MoveToTarget();
    }

    // Update is called once per frame
    void Update()
    {

        switch(EnemyState)
        {
            case EnemyStates.Patroling:
                if (OpponentAgent.remainingDistance <= EnemyRange && SettingNewTarget == false)
                {
                    SettingNewTarget = true;
                    StartCoroutine(SetNewTarget());
                    Debug.Log("CurrentTarget is " + GoldIndex);
                }
                else if(wasChasing)
                {
                    CurrentGoldTarget = GoldTargetsList[GoldIndex];
                    Invoke("MoveToTarget", 3.0f);
                }
                break;
            case EnemyStates.Chasing:
                {
                    StartChasingPlayer();
                }
                break;
        }
    }


    void StartChasingPlayer()
    {
        MoveToTarget();
    }


    IEnumerator SetNewTarget()
    {
        yield return new WaitForSecondsRealtime(EnemyWaitTime);
        GoldIndex++;
        if (GoldIndex >= GoldTargetsList.Count)
        {
            GoldIndex = 0;
        }   
        CurrentGoldTarget = GoldTargetsList[GoldIndex];
        MoveToTarget();
        SettingNewTarget = false;
    }

    void MoveToTarget()
    {
        OpponentAgent.SetDestination(CurrentGoldTarget.position);
    }






    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CurrentGoldTarget = other.gameObject.transform;
            EnemyState = EnemyStates.Chasing;
            StopAllCoroutines();
            Debug.Log("In Range and Detected");    
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EnemyState = EnemyStates.Patroling;
            Debug.Log("Out of Range");
            Debug.Log("Detected = False");
            wasChasing = true;
        }
    }



    private enum EnemyStates
    {
        Patroling,
        Chasing,
        Attacking
    }

}

