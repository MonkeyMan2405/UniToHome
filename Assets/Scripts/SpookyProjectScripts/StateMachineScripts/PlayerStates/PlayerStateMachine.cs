using Unity.VisualScripting;
using UnityEngine;
using static EnemyStateMachine;

// This is the state machine for the Player. It defines the different states that the enemy can be in and manages the transitions between those states.
public class PlayerStateMachine : StateManager<PlayerStateMachine.EPlayerState>
{ 

    public enum EPlayerState
    {
        //States
        Standard,
        Interacting,
        Hiding,
        Doomed,
        Working,
    }

    private PlayerStateContext _pContext;

    [SerializeField] public GameObject playerGameObject;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private CharacterController characterController;



    public void Awake()
    {
        _pContext = new PlayerStateContext();
        InitialiseStates();
    }

    private void InitialiseStates()
    {
        //Add the states to the state machine here. The key is the enum value and the value is the state class that corresponds to that enum value. Then set state

        States.Add(EPlayerState.Standard, new StandardState(_pContext, EPlayerState.Standard));
        States.Add(EPlayerState.Interacting, new InteractingState(_pContext, EPlayerState.Interacting));
        States.Add(EPlayerState.Hiding, new HidingState(_pContext, EPlayerState.Hiding));
        States.Add(EPlayerState.Doomed, new DoomedState(_pContext, EPlayerState.Doomed));
        States.Add(EPlayerState.Working, new WorkingState(_pContext, EPlayerState.Working));

        CurrentState = States[EPlayerState.Standard];
    }
}
