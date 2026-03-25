using UnityEngine;

public abstract class PlayerState : BaseState<PlayerStateMachine.EPlayerState>
{
    protected PlayerStateContext PContext;

    // The PlayerState class is an abstract class that represents a state in the enemy's state machine. It inherits from the BaseState class, which provides the basic functionality for a state in a state machine. The PlayerState class contains a reference to the PlayerStateContext, which is used to access the necessary information for the state to function properly.
    public PlayerState(PlayerStateContext _pContext, PlayerStateMachine.EPlayerState stateKey) : base(stateKey)
    {
        // Initialize the state with the context. The context contains all the necessary information for the state to function properly
        PContext = _pContext;
    }
}
