using UnityEngine;

public class MonsterThinkingState : MonsterState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public MonsterThinkingState(MonsterStateContext _mcontext, MonsterStateMachine.EMonsterState state) : base(_mcontext, state)
    {
        MonsterStateContext MContext = _mcontext;
    }

    public override void EnterState()
    {
        
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
