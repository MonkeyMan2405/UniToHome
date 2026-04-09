using Unity.VisualScripting;
using UnityEngine;

public class MonsterHallwayState : MonsterState
{

    private float walkTimer;
    private float countdownTimer;
    private float timer;
    private float doomTimer;

    private bool startDoomTimer;
    private bool shouldPeek;
    private bool hallwayWalking;
    private bool seenPlayer;
    private bool switchToThinkingState;


    private Vector3 monsterStartPos = new Vector3(6.24f, 0f, -11.5f);

    public MonsterHallwayState(MonsterStateContext _mcontext, MonsterStateMachine.EMonsterState state) : base(_mcontext, state)
    {
        MonsterStateContext MContext = _mcontext;
    }

    public override void EnterState()
    {
        Debug.Log("Entered Hallway State");
        MContext.monsterHallway.SetActive(true);

        MonsterStateMachine.monsterDanger = 10;
        MonsterStateMachine.monsterMin = 0;

        hallwayWalking = true;

        MContext.monsterHallway.transform.position = monsterStartPos;


    }



    public override void UpdateState()
    {

        HallwayWalkTime();

        SetupPeek();

        DoomAndDangerTimerAlt();



    }



    public override void ExitState()
    {
        shouldPeek = false;
        startDoomTimer = false;
        seenPlayer = false;

        switchToThinkingState = false;
        timer = 0;

        //disable just in case jumpscared early
        MContext.monsterHallDoor.SetActive(false);
        MContext.monsterHallway.SetActive(false);

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
        if (switchToThinkingState == true)
        {
            return MonsterStateMachine.EMonsterState.Thinking;
        }
        else if (MonsterStateMachine.triggerDoom == true)
        {
            return MonsterStateMachine.EMonsterState.Doom;
        }
            return StateKey;
    }



    public void HallwayWalkTime()
    {
        if (hallwayWalking == true)
        {
            walkTimer += Time.deltaTime;
            //be cautious with this number, it is timed with the monster's current speed. Initially tried a trigger but wasn't working, so this was quick but not optimal solution, I'll revisit in the future
            if (walkTimer >= 31)
            {
                Debug.Log("hall timer up");
                hallwayWalking = false;
                shouldPeek = true;

                walkTimer = 0;

                MContext.monsterHallway.SetActive(false);
                SetupRoomDoorAndMonster();

            }
        }

       
    }


    public void SetupRoomDoorAndMonster()
    {
        if (MContext.doorTriggerRef.doorRef.open == true)
        {
            MContext.doorTriggerRef.Interact();
        }
        MContext.monsterHallDoor.SetActive(true);
    }



    public void SetupPeek()
    {
        // once M is at door
        if (shouldPeek == true)
        {
            countdownTimer += Time.deltaTime;
            if (countdownTimer >= 6f)
            {
                countdownTimer = 0;
                shouldPeek = false;
                startDoomTimer = true;

                //if the door is closed, open it
                if (MContext.doorTriggerRef.doorRef.open == false)
                {
                    MContext.doorTriggerRef.doorRef.Interact();
                }
            }
        }

        else if (startDoomTimer == true)
        {
            StartVisionRaycast();
            doomTimer += Time.deltaTime;

            //door open before death
            if (doomTimer > 3f)
            {
                doomTimer = 0;
                if (seenPlayer == false)
                {
                    Leave();
                }
                else
                {
                    MonsterStateMachine.triggerDoom = true;
                }
            }
        }

    }

    public void DoomAndDangerTimerAlt()
    {
        timer += Time.deltaTime;

        if (timer >= MContext.patience + 6.5f)
        {
            MonsterStateMachine.monsterDanger = -1f;
        }

        else if (timer > 35f)
        {
            MonsterStateMachine.monsterDanger = 1.75f;
        }
        else if (timer > 20f)
        {
            MonsterStateMachine.monsterDanger = 4f;
        }
        else if (timer > 10f)
        {
            MonsterStateMachine.monsterDanger = 8;
        }
    }

    public void StartVisionRaycast()
    {

        //do not need to aim towards player as the head has a script which does this already
        Ray monsterVisionRay = new Ray(MContext.monsterHeadTransform.position, MContext.monsterHeadTransform.forward);

        if (Physics.Raycast(monsterVisionRay, out RaycastHit monsterVisionRayInfo, 20f, MContext.monsterVisionLayerMask))
        {
            Debug.DrawRay(MContext.monsterHeadTransform.position, MContext.monsterHeadTransform.forward * monsterVisionRayInfo.distance, Color.red);
            if (monsterVisionRayInfo.collider.CompareTag("Player"))
            {
                seenPlayer = true;

            }
        }
        
    }

    public void Leave()
    {
        //close door and leave, switch state
        MContext.doorTriggerRef.doorRef.Interact();
        switchToThinkingState = true;
    }


}
