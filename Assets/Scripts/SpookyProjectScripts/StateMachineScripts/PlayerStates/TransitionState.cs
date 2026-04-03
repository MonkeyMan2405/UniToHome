using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class TransitionState : PlayerState
{
    private Vector3 computerLocationWithOffset;
    private Vector3 oldPos;
    private Vector3 localNewPos;

    private Quaternion newCamRotation;
    private Quaternion oldCamRotation;

    private float lerpSpeed = 5f;
    private float zPosOffset = 1.3f;
    private float yPosOffset = 0.45f;

    private bool changeToStandardState;
    private bool changeToWorkingState;


    public TransitionState(PlayerStateContext _pcontext, PlayerStateMachine.EPlayerState state) : base(_pcontext, state)
    {
        PlayerStateContext PContext = _pcontext;
    }


    public override void EnterState()
    {
        PContext.headBobbingRef.enabled = false;

        newCamRotation = (Quaternion.Euler(0f, 0f, 0f));

        computerLocationWithOffset = PContext.newCamPos.position + new Vector3(0, -yPosOffset, -1.4f);
        //Set the new cam pos to infront of the computer
        localNewPos = PContext.newCamPos.position + new Vector3(0, -yPosOffset, -zPosOffset);


        //save old cam pos and rotation to use and return to when exiting state
        if (PContext.transitionIdentifier == 0)
        {
            oldPos = PContext.playerGameObject.transform.position;
            oldCamRotation = PContext.playerCamera.transform.rotation;
        }

    }



    public override void UpdateState()
    {

        //Check which Transition to Perform
        if (PContext.transitionIdentifier == 0)
        {
            CamComputerLerp();
        }
        else if (PContext.transitionIdentifier ==1)
        {
            PContext.verticalRotation = 0f;
            CamComputerExitLerp();
        }
        


    }



    public override void ExitState()
    {
        changeToWorkingState = false;
        changeToStandardState = false;
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

        if (changeToWorkingState == true)
        {
            return PlayerStateMachine.EPlayerState.Working;
        }

        else if (changeToStandardState == true)
        {
            return PlayerStateMachine.EPlayerState.Standard;
        }

            return StateKey;
    }


    private void CamComputerLerp()
    {
        PContext.playerGameObject.transform.position = Vector3.Lerp(PContext.playerGameObject.transform.position, localNewPos, lerpSpeed * Time.deltaTime);
        PContext.playerGameObject.transform.rotation = Quaternion.Lerp(PContext.playerGameObject.transform.rotation, newCamRotation, lerpSpeed * Time.deltaTime);
        PContext.playerCamera.transform.rotation = Quaternion.Lerp(PContext.playerCamera.transform.rotation, newCamRotation, lerpSpeed * Time.deltaTime);

        if (Vector3.Distance(PContext.playerGameObject.transform.position, localNewPos) < 0.01f)
        {
            changeToWorkingState = true;
        }
    }

    private void CamComputerExitLerp()
    {
        PContext.playerGameObject.transform.position = Vector3.Lerp(PContext.playerGameObject.transform.position, oldPos, lerpSpeed * Time.deltaTime);
        PContext.playerCamera.transform.rotation = Quaternion.Lerp(PContext.playerCamera.transform.rotation, newCamRotation, lerpSpeed * Time.deltaTime);

        if (Vector3.Distance(PContext.playerGameObject.transform.position, oldPos) < 0.01f)
        {
            changeToStandardState = true;
        }
    }


}
