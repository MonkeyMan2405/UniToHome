using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MonsterDoomState : MonsterState
{

    public static UnityEvent PlayerDoom = new UnityEvent();

    public MonsterDoomState(MonsterStateContext _mcontext, MonsterStateMachine.EMonsterState state) : base(_mcontext, state)
    {
        MonsterStateContext MContext = _mcontext;
    }

    public override void EnterState()
    {
        Debug.Log("Entered Doom State");


        //disable player character movement and flashlight and camera,
        //enable doom camera and trigger flashing light,
        //and enable doom animation

        if (MonsterStateMachine.hallwayDoom == true)
        {
            MContext.monsterDoom.transform.position = MContext.monsterHallway.transform.position;
            MContext.monsterDoom.transform.rotation = MContext.monsterHallway.transform.rotation;
        }

        MContext.playerCamera.enabled = false;
        MContext.doomCamera.enabled = true;
        MContext.monsterDoom.SetActive(true);

        MContext.psmRef.enabled = false;
        MContext.playerCamera.enabled = false;

      
        MonsterStateMachine.monsterDanger = 1.75f;

        PlayerDoom?.Invoke();



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



    //Checked every frame
    public override MonsterStateMachine.EMonsterState GetNextState()
    {
        return StateKey;
    }

   
    public void Doom()
    {
        
    }


}
