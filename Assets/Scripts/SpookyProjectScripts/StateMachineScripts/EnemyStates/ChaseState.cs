using UnityEditor.Experimental.GraphView;
using UnityEditor.PackageManager;
using UnityEngine;

public class ChaseState : EnemyState
{
    private RaycastHit hitInfo;
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

    }



    public override void UpdateState()
    {
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
                Debug.DrawRay(Context.EnemyRb.transform.position, rayDirection * hitInfo.distance, Color.red);
                //Check if raycast hit player, if so, chase
                if (hitInfo.collider.CompareTag("Player"))
                {
                    Context.EnemyAgent.SetDestination(Context.targetTransform.position);

                    //chjange this so only checks for player, cos it only needs to, and if lose sight, then transition to poi
                }
            }      
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
}



    
      
        
        

