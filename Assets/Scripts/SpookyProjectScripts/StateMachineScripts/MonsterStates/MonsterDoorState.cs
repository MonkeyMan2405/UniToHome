using Unity.VisualScripting;
using UnityEngine;

public class MonsterDoorState : MonsterState
{
    private float timer;
    private float leaveTimer;
    private bool changeToThinkingState;

    public MonsterDoorState(MonsterStateContext _mcontext, MonsterStateMachine.EMonsterState state) : base(_mcontext, state)
    {
        MonsterStateContext MContext = _mcontext;
    }

    public override void EnterState()
    {
        Debug.Log("Entered Wardrobe/Door State");
        MContext.monsterWardrobe.SetActive(true);

        MonsterStateMachine.monsterDanger = 10;
        MonsterStateMachine.monsterMin = 0;

        WardrobeSetup();

    }

    public override void UpdateState()
    {

        DoomAndDangerTimer();
        CheckDoorLeave();

    }



    public override void ExitState()
    {
        timer = 0;
        MonsterStateMachine.monsterDanger = -1;
        MonsterStateMachine.monsterMin = -1;

        MContext.wardrobeTriggerRef.monsterPresent = false;

        MContext.monsterWardrobe.SetActive(false);

        changeToThinkingState = false;
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
        if (changeToThinkingState == true)
        {
            return MonsterStateMachine.EMonsterState.Thinking;
        }
        return StateKey;
    }

    public void DoomAndDangerTimer()
    {
        timer += Time.deltaTime;

        if (timer >= MContext.patience)
        {
            Debug.Log("Doomed");
            MonsterStateMachine.monsterDanger = -1f;
        }

        else if (timer > 30f)
        {
            MonsterStateMachine.monsterDanger = 1.75f;
        }
        else if (timer > 20f)
        {
            MonsterStateMachine.monsterDanger = 4f;
        }
        else if (timer > 10f)
        {
            MonsterStateMachine.monsterDanger = 8;
        }

    }

    public void CheckDoorLeave()
    {
        //only need to check one, as if one is closed, so is the other.
        if (MContext.wardrobeTriggerRef.door2Ref.open == false)
        {
            leaveTimer += Time.deltaTime;
            if(leaveTimer >= 3f)
            {
                changeToThinkingState = true;
                leaveTimer = 0;
            }
        }
    }

    public void WardrobeSetup()
    {
        MContext.wardrobeTriggerRef.door2Ref.DoorOpenAngle = -20;
        MContext.wardrobeTriggerRef.door3Ref.DoorOpenAngle = 20;


        if (MContext.wardrobeTriggerRef.door2Ref.open == true)
        {
            MContext.wardrobeTriggerRef.MonsterInteract();
        }
        else
        {
            MContext.wardrobeTriggerRef.MonsterInteract();
        }

        MContext.wardrobeTriggerRef.monsterPresent = true;
    }

}
