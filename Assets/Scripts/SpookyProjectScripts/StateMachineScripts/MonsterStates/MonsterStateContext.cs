using UnityEngine;

public class MonsterStateContext
{

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

    public bool hallwayDoom;

    public WardrobeTrigger wardrobeTriggerRef;
    public DoorTrigger doorTriggerRef;


    [Header("AI and Difficulty")]

    public float thinkingTime;

    //How long it will wait before dooming
    public float patience;




    public MonsterStateContext
    (
        Transform playerTransform,
        PlayerStateMachine psmRef,
        Camera playerCamera,
        Camera doomCamera,  

        Transform monsterHeadTransform,

        LayerMask monsterVisionLayerMask,

        GameObject monsterHallway,
        GameObject monsterHalldoor,
        GameObject monsterWindow,
        GameObject monsterWardrobe,

        GameObject monsterDoom,
   
        WardrobeTrigger wardrobeTriggerRef,
        DoorTrigger doorTriggerRef,

        float thinkingTime,
        float patience

    )


    {
        this.playerTransform = playerTransform;
        this.psmRef = psmRef;
        this.playerCamera = playerCamera;
        this.doomCamera = doomCamera;

        this.monsterHeadTransform = monsterHeadTransform;

        this.monsterVisionLayerMask = monsterVisionLayerMask;

        this.monsterHallway = monsterHallway;
        this.monsterWindow = monsterWindow;
        this.monsterHallDoor = monsterHalldoor;
        this.monsterWardrobe = monsterWardrobe;

        this.monsterDoom = monsterDoom;

        this.wardrobeTriggerRef = wardrobeTriggerRef;
        this.doorTriggerRef = doorTriggerRef;

        this.thinkingTime = thinkingTime;
        this.patience = patience;

    }

}
