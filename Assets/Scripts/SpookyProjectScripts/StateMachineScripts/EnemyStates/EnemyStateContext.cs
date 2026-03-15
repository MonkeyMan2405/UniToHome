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
    public float rayCheckDistance;
    public LayerMask layerMask;

    public RaycastHit hitInfo;

    public MeshRenderer enemyMeshRenderer;
    public Material enemyFaceMaterial;

    public int rayCount;
    public float raySpacing;     



    public EnemyStateContext(Rigidbody enemyRb, NavMeshAgent enemyAgent, Transform targetTransform, Vector3 playerLastLocation, List<Transform> patrolPointsList, float rayCheckDistance, LayerMask layerMask, RaycastHit hitInfo, MeshRenderer enemyMeshRenderer, Material enemyFaceMaterial, int rayCount, float raySpacing)
    {
        // Initialize the context with the necessary variables for the enemy's state machine.
        this.enemyRb = enemyRb;
        this.enemyAgent = enemyAgent;
        this.targetTransform = targetTransform;
        this.playerLastLocation = playerLastLocation;
        this.patrolPointsList = patrolPointsList;
        this.rayCheckDistance = rayCheckDistance;
        this.layerMask = layerMask;
        this.hitInfo = hitInfo;
        this.enemyMeshRenderer = enemyMeshRenderer;
        this.enemyFaceMaterial = enemyFaceMaterial;
        this.rayCount = rayCount;
        this.raySpacing = raySpacing;

    }

    // Getters for the context variables. These can be used by the different states to access the necessary information about the enemy and its environment.
    public Rigidbody EnemyRb  => enemyRb;
    public NavMeshAgent EnemyAgent => enemyAgent;
    public Transform TargetTransform => targetTransform;    
    public Vector3 PlayerLastLocation => playerLastLocation;
    public List<Transform> PatrolPointsList => patrolPointsList;


}
