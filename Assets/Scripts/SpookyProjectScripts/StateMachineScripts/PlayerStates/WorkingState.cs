using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class WorkingState : PlayerState
{
    private bool changeToTransitionState;
   
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            changeToTransitionState = true;
            PContext.transitionIdentifier = 1;
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
