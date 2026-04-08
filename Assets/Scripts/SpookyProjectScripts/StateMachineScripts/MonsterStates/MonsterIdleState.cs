using UnityEngine;

public class MonsterIdleState : MonsterState
{

    private float thinkingTimer;
    private float thinkingChange;

    public MonsterIdleState(MonsterStateContext _mcontext, MonsterStateMachine.EMonsterState state) : base(_mcontext, state)
    {
        MonsterStateContext MContext = _mcontext;
    }

    public override void EnterState()
    {
        thinkingChange = Random.Range(20, MContext.thinkingTime);
    }



    public override void UpdateState()
    {
       thinkingTimer += Time.deltaTime;
       if(thinkingTimer >= thinkingChange)
       {
           
       }
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
