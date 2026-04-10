using UnityEngine;
using UnityEngine.Events;

public class WorkingState : PlayerState
{
    private bool changeToTransitionState;

    public static UnityEvent GeneratePattern = new UnityEvent();
    public static UnityEvent<int> MonitorInput = new UnityEvent<int>();

    public int directionInput;

    public WorkingState(PlayerStateContext _pcontext, PlayerStateMachine.EPlayerState state) : base(_pcontext, state)
    {
        PlayerStateContext PContext = _pcontext;
    }

    public override void EnterState()
    {

        Debug.Log("Working");

    }



    public override void UpdateState()
    {
        if (Input.GetMouseButtonDown(0))
        {
            changeToTransitionState = true;
            PContext.transitionIdentifier = 1;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            directionInput = 1;
            MonitorInput?.Invoke(directionInput);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            directionInput = 4;
            MonitorInput?.Invoke(directionInput);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            directionInput = 3;
            MonitorInput?.Invoke(directionInput);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            directionInput = 2;
            MonitorInput?.Invoke(directionInput);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            GeneratePattern?.Invoke();
        }

    }



    public override void ExitState()
    {
        changeToTransitionState = false;
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




    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (changeToTransitionState == true)
        {
            return PlayerStateMachine.EPlayerState.Transition;
        }
        return StateKey;
    }

}
