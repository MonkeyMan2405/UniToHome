using UnityEngine;

public class MonsterStateMachine : StateManager<MonsterStateMachine.EMonsterState>
{

    public static float monsterDanger = -1;
    public static float monsterMin = -1;
    public static bool triggerDoom;
    public static bool hallwayDoom;
    public static bool wardrobeDoom;
    public enum EMonsterState
    {
        Thinking,
        Hallway,
        Window,
        Door,
        Doom,
    }

    private MonsterStateContext _mContext;
    
    //variables

    [Header("References")]

    public Transform playerTransform;
    public PlayerStateMachine psmRef;
    public Camera playerCamera;
    public Camera doomCamera;

    public Transform monsterHeadTransform;

    public LayerMask monsterVisionLayerMask;

    public GameObject monsterHallway;
    public GameObject monsterHallDoor;
    public GameObject monsterWindow;
    public GameObject monsterWardrobe;

    public GameObject monsterDoom;


    public WardrobeTrigger wardrobeTriggerRef;
    public DoorTrigger doorTriggerRef;

    [Header("AI and Difficulty")]

    //how long the monster will think before doing something
    public float thinkingTime;

    //How long it will wait before dooming
    public float patience;



    private void Awake()
    {
        // try this: _mContext = GetComponent<MonsterStateContext>();

        _mContext = new MonsterStateContext(playerTransform, psmRef, playerCamera, doomCamera, monsterHeadTransform, monsterVisionLayerMask, monsterHallway, monsterHallDoor, monsterWindow, monsterWardrobe, monsterDoom, wardrobeTriggerRef, doorTriggerRef, thinkingTime, patience);

        InitialiseStates();
    }
    


    private void InitialiseStates()
    {
        States.Add(EMonsterState.Thinking, new MonsterThinkingState(_mContext, EMonsterState.Thinking));
        States.Add(EMonsterState.Hallway, new MonsterHallwayState(_mContext, EMonsterState.Hallway));
        States.Add(EMonsterState.Window, new MonsterWindowState(_mContext, EMonsterState.Window));
        States.Add(EMonsterState.Door, new MonsterDoorState(_mContext, EMonsterState.Door));
        States.Add(EMonsterState.Doom, new MonsterDoomState(_mContext, EMonsterState.Doom));

        CurrentState = States[EMonsterState.Thinking];
    }

}
