using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class WorkingState : PlayerState
{
    private bool changeToTransitionState;

    public static UnityEvent GeneratePattern = new UnityEvent();
    public static UnityEvent<int> MonitorInput = new UnityEvent<int>();

    public int directionInput;

    InputAction interact;
    InputAction interact2;

    InputAction MoveAction;



    public WorkingState(PlayerStateContext _pcontext, PlayerStateMachine.EPlayerState state) : base(_pcontext, state)
    {
        PlayerStateContext PContext = _pcontext;
    }

    public override void EnterState()
    {

        Debug.Log("Working");


        interact = InputSystem.actions.FindAction("Interact");
        interact2 = InputSystem.actions.FindAction("Interact2");
        MoveAction = InputSystem.actions.FindAction("Move");
      

    }



    public override void UpdateState()
    {

          Vector2 movement = MoveAction.ReadValue<Vector2>();

          Debug.LogError("X: " + movement.x);
          Debug.LogError("Y: " + movement.y);


        if (interact.WasPressedThisFrame())
        {
            changeToTransitionState = true;
            PContext.transitionIdentifier = 1;
        }
        else if (movement.y >= 0.1f)
        {
            directionInput = 1;
            MonitorInput?.Invoke(directionInput);
        }
        else if (movement.x <= -0.1f)
        {
            directionInput = 4;
            MonitorInput?.Invoke(directionInput);
        }
        else if (movement.y <= -0.1f)
        {
            directionInput = 3;
            MonitorInput?.Invoke(directionInput);
        }
        else if (movement.x >= 0.1f)
        {
            directionInput = 2;
            MonitorInput?.Invoke(directionInput);
        }
        else if (interact2.WasPressedThisFrame())
        {
            GeneratePattern?.Invoke();
        }

    }



    public override void ExitState()
    {
        changeToTransitionState = false;
    }





    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (changeToTransitionState == true)
        {
            return PlayerStateMachine.EPlayerState.Transition;
        }
        return StateKey;
    }

}
