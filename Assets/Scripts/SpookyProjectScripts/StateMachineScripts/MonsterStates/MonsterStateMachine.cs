using UnityEngine;

public class MonsterStateMachine : StateManager<MonsterStateMachine.EMonsterState>
{

    public static float monsterDanger = -1;
    public static float monsterMin = -1;
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

    public GameObject monsterHallway;
    public GameObject monsterWindow;
    public GameObject monsterWardrobe;

    [Header("AI and Difficulty")]

    public float thinkingTime;

    //How long it will wait before dooming
    public float patience;



    private void Awake()
    {
        // try this: _mContext = GetComponent<MonsterStateContext>();

        _mContext = new MonsterStateContext(playerTransform, monsterHallway, monsterWindow, monsterWardrobe, thinkingTime, patience);

        InitialiseStates();
    }



    private void InitialiseStates()
    {
        States.Add(EMonsterState.Thinking, new MonsterThinkingState(_mContext, EMonsterState.Thinking));
        States.Add(EMonsterState.Idle, new MonsterIdleState(_mContext, EMonsterState.Idle));
        States.Add(EMonsterState.Hallway, new MonsterHallwayState(_mContext, EMonsterState.Hallway));
        States.Add(EMonsterState.Window, new MonsterWindowState(_mContext, EMonsterState.Window));
        States.Add(EMonsterState.Door, new MonsterDoorState(_mContext, EMonsterState.Door));

        CurrentState = States[EMonsterState.Thinking];
    }

}
