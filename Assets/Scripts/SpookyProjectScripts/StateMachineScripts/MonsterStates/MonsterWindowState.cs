using System.Collections;
using UnityEngine;

public class MonsterWindowState : MonsterState
{

    private float timer;
    private float leaveTimer;
    private bool changeToThinkingState;
    private Animator monsterWindowAnimator;
    private bool visitedBefore;

    public MonsterWindowState(MonsterStateContext _mcontext, MonsterStateMachine.EMonsterState state) : base(_mcontext, state)
    {
        MonsterStateContext MContext = _mcontext;
    }

    public override void EnterState()
    {
        Debug.Log("Entered Window State");
        MContext.monsterWindow.SetActive(true);

        monsterWindowAnimator = MContext.monsterWindow.GetComponentInChildren<Animator>();

        if (visitedBefore == true)
        {
            //enablke this again, as it was disabled once animation was completred to allow head looking
            monsterWindowAnimator.enabled = true;
        }
   

        MonsterStateMachine.monsterDanger = 10;
        MonsterStateMachine.monsterMin = 0;

    }


    public override void UpdateState()
    {

        DoomAndDangerTimer();
        CheckWindowLeave();

    }


    public override void ExitState()
    {
        timer = 0;
        MonsterStateMachine.monsterDanger = -1;
        MonsterStateMachine.monsterMin = -1;

        changeToThinkingState = false;

        visitedBefore = true;

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
        else if (MonsterStateMachine.triggerDoom == true)
        {
            return MonsterStateMachine.EMonsterState.Doom;
        }
        return StateKey;
    }


    //be careful when changing values, monster light clicker scripts relies on same values as 2nd if statement
    public void DoomAndDangerTimer()
    {
        timer += Time.deltaTime;
        
        if (timer >= MContext.patience)
        {
            MonsterStateMachine.triggerDoom = true;
            MonsterStateMachine.monsterDanger = -1f;
        }

        else if (timer >30f)
        {
            MonsterStateMachine.monsterDanger = 1.75f;
        }
        else if (timer >20f)
        {
            MonsterStateMachine.monsterDanger = 4f;
        }
        else if (timer >10f)
        {
            MonsterStateMachine.monsterDanger = 8;
        }
    
    }

    public void CheckWindowLeave()
    {
        if (PlayerStateMachine.blindsClosed == true)
        {
            leaveTimer += Time.deltaTime;
            if (leaveTimer >= 5f)
            {
                changeToThinkingState = true;
                leaveTimer = 0;
            }
        }
    }
   
}
