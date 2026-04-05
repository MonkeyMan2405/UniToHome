using UnityEngine;

public abstract class MonsterState : BaseState<MonsterStateMachine.EMonsterState>
{
    protected MonsterStateContext MContext;

    // The MonsterState class is an abstract class that represents a state in the Monster's state machine. It inherits from the BaseState class, which provides the basic functionality for a state in a state machine.
    // The MonsterState class contains a reference to the MonsterStateContext, which is used to access the necessary information for the state to function properly.

    public MonsterState(MonsterStateContext _mContext, MonsterStateMachine.EMonsterState stateKey) : base(stateKey)
    {
        // Initialize the state with the context. The context contains all the necessary information for the state to function properly
        MContext = _mContext;
    }
}
