using UnityEngine;

public class MonsterThinkingState : MonsterState
{
    private float timer;
    private int randomDecision;

    private bool changeToHallwayState;
    private bool changeToWindowState;
    private bool changeToWardrobeState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public MonsterThinkingState(MonsterStateContext _mcontext, MonsterStateMachine.EMonsterState state) : base(_mcontext, state)
    {
        MonsterStateContext MContext = _mcontext;
    }

    public override void EnterState()
    {
        Debug.Log("Entered Thinking State");
        MContext.monsterHallway.SetActive(false);
        MContext.monsterWindow.SetActive(false);
        MContext.monsterWardrobe.SetActive(false);
    }



    public override void UpdateState()
    {
        Thinking();
    }



    public override void ExitState()
    {
        //reset timer for next time it enters this state
        timer = 0f;

        changeToHallwayState = false;
        changeToWindowState = false;
        changeToWardrobeState = false;

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
        if (changeToHallwayState)
        {
            return MonsterStateMachine.EMonsterState.Hallway;
        }
        else if (changeToWindowState)
        {
            return MonsterStateMachine.EMonsterState.Window;
        }
        else if (changeToWardrobeState)
        {
            return MonsterStateMachine.EMonsterState.Door;
        }
        return StateKey;
    }



    public void Thinking()
    {
        timer += Time.deltaTime;

        if (timer >= MContext.thinkingTime)
        {   timer = 0f; 
            randomDecision = Random.Range(0, 2);
            //randomDecision = 2;
            MakeDecision();
        }
    }

    public void MakeDecision()
    {
        if (randomDecision == 0)
        {
           //do nothing
        }
        else if (randomDecision == 1)
        {
            changeToHallwayState = true;
        }
        else if (randomDecision == 2)
        {
            changeToWindowState = true;
        }
        else if (randomDecision == 3)
        {
            changeToWardrobeState = true;
        }
    }

}
