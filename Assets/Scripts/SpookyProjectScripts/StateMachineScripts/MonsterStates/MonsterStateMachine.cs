using UnityEngine;

public class MonsterStateMachine : StateManager<MonsterStateMachine.EMonsterState>
{

    public enum EMonsterState
    {
        Thinking,
        Idle,
        Hallway,
        Window,
        Door,
    }

    private MonsterStateContext _mContext;
    
    //variables

    [Header("References")]

    public Transform playerTransform;







    private void Awake()
    {
        // try this: _mContext = GetComponent<MonsterStateContext>();

        _mContext = new MonsterStateContext();

        InitialiseStates();
    }



    private void InitialiseStates()
    {
        States.Add(EMonsterState.Thinking, new MonsterThinkingState(_mContext, EMonsterState.Thinking));
        States.Add(EMonsterState.Idle, new MonsterIdleState(_mContext, EMonsterState.Idle));
        States.Add(EMonsterState.Hallway, new MonsterHallwayState(_mContext, EMonsterState.Hallway));
        States.Add(EMonsterState.Window, new MonsterWindowState(_mContext, EMonsterState.Window));
        States.Add(EMonsterState.Door, new MonsterDoorState(_mContext, EMonsterState.Door));
    }

}
