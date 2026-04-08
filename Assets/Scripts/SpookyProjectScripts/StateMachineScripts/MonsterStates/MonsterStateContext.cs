using UnityEngine;

public class MonsterStateContext
{

    [Header("References")]

    public Transform playerTransform;
    public PlayerStateMachine psmRef;

    public GameObject monsterHallway;
    public GameObject monsterWindow;
    public GameObject monsterWardrobe;

    public WardrobeTrigger wardrobeTriggerRef;


    [Header("AI and Difficulty")]

    public float thinkingTime;

    //How long it will wait before dooming
    public float patience;




    public MonsterStateContext
    (
        Transform playerTransform,
        PlayerStateMachine psmRef,

        GameObject monsterHallway,
        GameObject monsterWindow,
        GameObject monsterWardrobe,

        WardrobeTrigger wardrobeTriggerRef,

        float thinkingTime,
        float patience

    )


    {
        this.playerTransform = playerTransform;
        this.psmRef = psmRef;

        this.monsterHallway = monsterHallway;
        this.monsterWindow = monsterWindow;
        this.monsterWardrobe = monsterWardrobe;

        this.wardrobeTriggerRef = wardrobeTriggerRef;

        this.thinkingTime = thinkingTime;
        this.patience = patience;

    }

}
