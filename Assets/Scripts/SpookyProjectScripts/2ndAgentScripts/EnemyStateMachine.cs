using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// This is the state machine for the enemy. It defines the different states that the enemy can be in and manages the transitions between those states.
public class EnemyStateMachine : StateManager<EnemyStateMachine.EEnemyState>
{
    public enum EEnemyState
    {
        //States
        Patrol,
        Chase,
        SearchPoi,
    }

    private EnemyStateContext _context;

    [SerializeField] private Rigidbody enemyRb;
    [SerializeField] private NavMeshAgent enemyAgent;
    [SerializeField] public Transform targetTransfrom;
    [SerializeField] public Vector3 playerLastLocation;
    [SerializeField] public List<Transform> patrolPointsList;
    //Whatever other variables the enemy needs to keep track of can be added here.


    



    private void Awake()
    {
        _context = new EnemyStateContext(enemyRb, enemyAgent, targetTransfrom, playerLastLocation, patrolPointsList);
        InitialiseStates();
    }

    private void InitialiseStates()
    {
        //Add the states to the state machine here. The key is the enum value and the value is the state class that corresponds to that enum value. Then set state
        States.Add(EEnemyState.Patrol, new PatrolState(_context, EEnemyState.Patrol));
        States.Add(EEnemyState.Chase, new ChaseState(_context, EEnemyState.Chase));
        States.Add(EEnemyState.SearchPoi, new SeachPoiState(_context, EEnemyState.SearchPoi));
        CurrentState = States[EEnemyState.Patrol];
    }


}

