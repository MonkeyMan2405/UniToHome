using System.Net.Mail;
using Unity.VisualScripting;
using UnityEngine;

public class SeachPoiState : EnemyState
{
    [SerializeField]
    private Transform lastSeenPoiTransform;
    private bool seenPlayer = false;
    private float attentionTimer;

    RaycastHit hitInfo;
    private float rayStartAngle;
    private float rayAngle;
    private Quaternion rayRotation;
    private Vector3 rayDirection;



    public SeachPoiState(EnemyStateContext context, EnemyStateMachine.EEnemyState state) : base(context, state)
    {
        EnemyStateContext Context = context;
    }



    public override void EnterState()
    {
        Debug.Log("Entered Poi State");
        lastSeenPoiTransform = Context.TargetTransform;
        attentionTimer = 0f;

        seenPlayer = false;
        SearchLastPoi();
    }




    public override void UpdateState()
    {    
         RaySearchForPlayer();
    }




    public override void ExitState()
    {

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
        if (attentionTimer >= 5f)
        {
            return EnemyStateMachine.EEnemyState.Patrol;
        }
        else if (seenPlayer == true)
        {
            return EnemyStateMachine.EEnemyState.Chase;
        }
        return StateKey;
    }



    public void SearchLastPoi()
    {
        Context.EnemyAgent.SetDestination(lastSeenPoiTransform.position);
    }

    public void RaySearchForPlayer()
    {
          //Raycast Cone
        Context.raySpacing = 12f;
        Context.rayCount = 12;

        Ray enemyRay = new Ray(Context.EnemyRb.transform.position, Context.EnemyRb.transform.forward);

        for (int i = 0; i < Context.rayCount; i++)
        {
            float rayStartAngle = -(Context.raySpacing * Context.rayCount - 1) / 2f;
            //Calculate angle for the rays
            float rayAngle = rayStartAngle + (i * Context.raySpacing);
            rayRotation = Quaternion.Euler(0, rayAngle, 0);
            rayDirection = rayRotation * Context.EnemyRb.transform.forward;

            // Perform the raycast. Red and blocked if hitting an object, green if not.

            if (Physics.Raycast(Context.EnemyRb.transform.position, rayDirection, out hitInfo, Context.rayCheckDistance))
            {
                Debug.DrawRay(Context.EnemyRb.transform.position, rayDirection * hitInfo.distance, Color.green);

                //check if the raycast hit the player
                if (hitInfo.collider.CompareTag("Player"))
                {
                    Debug.DrawRay(Context.EnemyRb.transform.position, rayDirection * hitInfo.distance, Color.red);
                    seenPlayer = true;
                }
            }
        }
        Debug.Log("Attention Timer: " + attentionTimer);
        attentionTimer += Time.deltaTime;
    }

}
