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

        MContext.monsterHallway.SetActive(false);
        MContext.monsterHallDoor.SetActive(false);
        MContext.monsterWindow.SetActive(false);
        MContext.monsterWardrobe.SetActive(false);

        if (MonsterStateMachine.hallwayDoom == true)
        {
            MContext.monsterDoom.transform.position = MContext.monsterHallway.transform.position;
            MContext.monsterDoom.transform.rotation = MContext.monsterHallway.transform.rotation;
        }
        else if (MonsterStateMachine.wardrobeDoom == true)
        {
            MContext.monsterDoom.transform.position = MContext.monsterWardrobe.transform.position - new Vector3(-1.1f, 0, 0.7f);
            MContext.monsterDoom.transform.rotation = MContext.monsterWardrobe.transform.rotation;
        }


        //disable player character movement and flashlight and camera,
        //enable doom camera and trigger flashing light,
        //and enable doom animation

        MContext.playerCamera.enabled = false;
        MContext.doomCamera.enabled = true;
        MContext.monsterDoom.SetActive(true);

        MContext.psmRef.enabled = false;
        MContext.playerCamera.enabled = false;

      
        MonsterStateMachine.monsterDanger = 1.75f;
        MonsterStateMachine.triggerDoom = false;

        PlayerDoom?.Invoke();



    }


    public override void UpdateState()
    {



    }


    public override void ExitState()
    {
     

    }





    //Checked every frame
    public override MonsterStateMachine.EMonsterState GetNextState()
    {
        return StateKey;
    }



}
