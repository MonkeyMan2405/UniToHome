using UnityEngine;

public class SeachPoiState : EnemyState
{
    [SerializeField]
    private Transform lastSeenPoiTransform;


    public SeachPoiState(EnemyStateContext context, EnemyStateMachine.EEnemyState state) : base(context, state)
    {
        EnemyStateContext Context = context;
    }



    public override void EnterState()
    {
        Debug.Log("Entered Poi State");
        lastSeenPoiTransform = Context.TargetTransform;
        SearchLastPoi();
    }




    public override void UpdateState()
    {

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
        return StateKey;
    }



    public void SearchLastPoi()
    {
        Context.EnemyAgent.SetDestination(lastSeenPoiTransform.position);
    }
}
