using UnityEngine;

public class WorkingState : PlayerState
{


    public WorkingState(PlayerStateContext _pcontext, PlayerStateMachine.EPlayerState state) : base(_pcontext, state)
    {
        PlayerStateContext PContext = _pcontext;
    }

    public override void EnterState()
    {

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




    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        return StateKey;
    }
}
