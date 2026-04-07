using System.Collections;
using UnityEngine;

public class MonsterWindowState : MonsterState
{

    private Transform windowInitialPos;
    private float timer;
    private float flickerRdm;

    public MonsterWindowState(MonsterStateContext _mcontext, MonsterStateMachine.EMonsterState state) : base(_mcontext, state)
    {
        MonsterStateContext MContext = _mcontext;
    }

    public override void EnterState()
    {
        Debug.Log("Entered Window State");
        MContext.monsterWindow.SetActive(true);
        windowInitialPos = MContext.monsterWindow.transform;

        MonsterStateMachine.monsterDanger = 10;
        MonsterStateMachine.monsterMin = 0;

    }


    public override void UpdateState()
    {

        DoomAndDangerTimer();

    }

    public override void ExitState()
    {
        timer = 0;
        MonsterStateMachine.monsterDanger = -1;
        MonsterStateMachine.monsterMin = -1;
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



    public void DoomAndDangerTimer()
    {
        timer += Time.deltaTime;
        
        if (timer >= MContext.patience)
        {
            Debug.Log("Doomed");
            MonsterStateMachine.monsterDanger = -1f;
        }
        else if (timer > 40f)
        {
            MonsterStateMachine.monsterDanger = 1.75f;
        }
        else if (timer >30f)
        {
             MonsterStateMachine.monsterDanger = 4f;
        }
         else if (timer >20f)
        {
             MonsterStateMachine.monsterDanger = 7f;
        }
         else if (timer >10f)
        {
             MonsterStateMachine.monsterDanger = 8;
        }

       // if (timer >= MContext.patience)
    
    }
   
}
