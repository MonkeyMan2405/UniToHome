using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class SecondAgentMover : MonoBehaviour
{
    public Transform currentTarget;

    private NavMeshAgent agent;

    public List<Transform> targetsList;

    private Vector3 playerLastLocation;

    [SerializeField]
    private float closingRange = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget != null)
        {
            agent.SetDestination(currentTarget.position);
        }
    }
}
