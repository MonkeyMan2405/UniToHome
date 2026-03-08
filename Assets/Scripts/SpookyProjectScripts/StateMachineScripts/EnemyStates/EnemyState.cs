using UnityEngine;

public abstract class EnemyState : BaseState<EnemyStateMachine.EEnemyState>
{
   protected EnemyStateContext Context;

    // The EnemyState class is an abstract class that represents a state in the enemy's state machine. It inherits from the BaseState class, which provides the basic functionality for a state in a state machine. The EnemyState class contains a reference to the EnemyStateContext, which is used to access the necessary information for the state to function properly.
    public EnemyState(EnemyStateContext context, EnemyStateMachine.EEnemyState stateKey) : base(stateKey)
    {
        // Initialize the state with the context. The context contains all the necessary information for the state to function properly, such as references to the enemy's Rigidbody, NavMeshAgent, target transform, player last location, and patrol points list.
        Context = context;
    }
}
