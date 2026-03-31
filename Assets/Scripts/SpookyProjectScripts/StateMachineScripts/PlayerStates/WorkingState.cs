using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class WorkingState : PlayerState
{
    public Transform oldCamPos;
    public Camera transitionCamera;
    private float lerpSpeed = 10f;
    private float camOffset = 1f;

    public WorkingState(PlayerStateContext _pcontext, PlayerStateMachine.EPlayerState state) : base(_pcontext, state)
    {
        PlayerStateContext PContext = _pcontext;
    }

    public override void EnterState()
    {
        PContext.headBobbingRef.enabled = false;
        Debug.Log("Working");
        
        // oldCamPos.position = PContext.camPivotRef.transform.position;

        PContext.newCamPos.position += new Vector3(0, 0, -camOffset);

        PContext.camPivotRef.transform.position = PContext.newCamPos.position;
        PContext.playerCamera.transform.rotation = camrotation;

        //PContext.actualPlayerCamera.transform.position = PContext.newCamPos.position;
        

        //Enable the transition cam before disabling main
        // PContext.workCamera.enabled = true;
        // PContext.actualPlayerCamera.enabled = false;
        
    }



    public override void UpdateState()
    {
        //PContext.actualPlayerCamera.transform.position = Vector3.Lerp(oldCamPos.transform.position, PContext.newCamPos.position, lerpSpeed * Time.deltaTime);
        
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
