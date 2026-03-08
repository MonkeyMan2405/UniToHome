using UnityEngine;

public class PatrolState : EnemyState 
{
    public PatrolState(EnemyStateContext context, EnemyStateMachine.EEnemyState state) : base(context, state)
    {
        EnemyStateContext Context = context;
    }

    public override void EnterState()
    {
        Debug.Log("Entered Patrol State");
    }

    public override void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
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
            return EnemyStateMachine.EEnemyState.Chase;
        }
        return StateKey;
    }
}

   