using UnityEngine;

public class ChaseState : EnemyState
{
    public ChaseState(EnemyStateContext context, EnemyStateMachine.EEnemyState state) : base(context, state)
    {
        EnemyStateContext Context = context;
    }



    public override void EnterState()
    {
        Debug.Log("Entered Chase State");
    }



    public override void UpdateState()
    {
        Context.EnemyAgent.SetDestination(Context.TargetTransform.position);
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            return EnemyStateMachine.EEnemyState.SearchPoi;
        }
        return StateKey;
    }
}
