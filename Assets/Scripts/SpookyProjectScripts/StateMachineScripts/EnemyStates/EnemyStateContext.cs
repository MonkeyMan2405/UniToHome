using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateContext
{
    
    [SerializeField] private Rigidbody enemyRb;
    [SerializeField] private NavMeshAgent enemyAgent;
    public Transform targetTransform;
    public Vector3 playerLastLocation;
    public List<Transform> patrolPointsList;

    public EnemyStateContext(Rigidbody enemyRb, NavMeshAgent enemyAgent, Transform targetTransform, Vector3 playerLastLocation, List<Transform> patrolPointsList)
    {
        // Initialize the context with the necessary variables for the enemy's state machine.
        this.enemyRb = enemyRb;
        this.enemyAgent = enemyAgent;
        this.targetTransform = targetTransform;
        this.playerLastLocation = playerLastLocation;
        this.patrolPointsList = patrolPointsList;
    }

    // Getters for the context variables. These can be used by the different states to access the necessary information about the enemy and its environment.
    public Rigidbody EnemyRb  => enemyRb;
    public NavMeshAgent EnemyAgent => enemyAgent;
    public Transform TargetTransform => targetTransform;    
    public Vector3 PlayerLastLocation => playerLastLocation;
    public List<Transform> PatrolPointsList => patrolPointsList;

}
