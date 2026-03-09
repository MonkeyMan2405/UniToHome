using UnityEngine;
using UnityEngine.AI;

public class AgentMover : MonoBehaviour
{

    public Transform Target;

    private NavMeshAgent Agent;

   
    void Start()
    {
        

        Agent = GetComponent<NavMeshAgent>();
    }

  
    void Update()
    {

        if (Target!= null)
        {
            Agent.SetDestination(Target.position);
        }
        
    }
}
