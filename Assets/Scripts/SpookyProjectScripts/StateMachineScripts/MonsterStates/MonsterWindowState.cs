using UnityEngine;

public class MonsterWindowState : MonsterState
{

    private Transform windowInitialPos;

    public MonsterWindowState(MonsterStateContext _mcontext, MonsterStateMachine.EMonsterState state) : base(_mcontext, state)
    {
        MonsterStateContext MContext = _mcontext;
    }

    public override void EnterState()
    {
        Debug.Log("Entered Window State");

        MContext.monsterWindow.SetActive(true);
        windowInitialPos = MContext.monsterWindow.transform;
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
}
