using UnityEngine;

public class MonsterStateContext
{

    [Header("References")]

    public Transform playerTransform;

    public GameObject monsterHallway;
    public GameObject monsterWindow;
    public GameObject monsterWardrobe;


    [Header("AI and Difficulty")]

    public float thinkingTime;

    //How long it will wait before dooming
    public float patience;




    public MonsterStateContext
    (
        Transform playerTransform,

        GameObject monsterHallway,
        GameObject monsterWindow,
        GameObject monsterWardrobe,

        float thinkingTime,
        float patience

    )


    {
        this.playerTransform = playerTransform;

        this.monsterHallway = monsterHallway;
        this.monsterWindow = monsterWindow;
        this.monsterWardrobe = monsterWardrobe;

        this.thinkingTime = thinkingTime;
        this.patience = patience;

    }

}
