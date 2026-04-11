using DoorScript;
using UnityEngine;

public class WardrobeTrigger : MonoBehaviour, IInteractable
{

    public Door2 door2Ref;
    public Door3 door3Ref;

    public bool monsterPresent;

    public float timer;


    private Collider wardrobeCollider;

    public void Start()
    {
        wardrobeCollider = GetComponent<Collider>();
    }


    public void Interact()
    {
        door2Ref.Interact();
        door3Ref.Interact();

        if (wardrobeCollider.isTrigger)
        {
            wardrobeCollider.isTrigger = false;
            SoundManager.PlaySoundAt(SoundType.ClosetClose, 1f, 0.5f, gameObject.transform);
        }
        else
        {
            wardrobeCollider.isTrigger = true;
            SoundManager.PlaySoundAt(SoundType.ClosetOpen, 1f, 0.5f, gameObject.transform);
        }

    }

    public void MonsterInteract()
    {
        door2Ref.MonsterInteract();
        door3Ref.MonsterInteract();

        if (wardrobeCollider.isTrigger)
        {
            wardrobeCollider.isTrigger = false;
            SoundManager.PlaySoundAt(SoundType.ClosetClose, 1f, 0.5f, gameObject.transform);
        }
        else
        {
            wardrobeCollider.isTrigger = true;
            SoundManager.PlaySoundAt(SoundType.ClosetOpen, 1f, 0.5f, gameObject.transform);
        }
    }


    public void Update()
    {
        CloseTimer();
    }

    public void CloseTimer()
    {
        //if door open, start timer, if hit limit, vlose doors, if closed, reset timer
        if (door2Ref.open == true)
        {
            //I'm not sure this does anything
            if (monsterPresent == false)
            {
                timer += Time.deltaTime;

                if (timer >= 2.5f)
                {
                    door2Ref.open = false;
                    door3Ref.open = false;
                    wardrobeCollider.isTrigger = false;
                    timer = 0f;
                }
            }
            else
            {
                //do nothing, let monster be
            }

        }
        else
        {
       
        }
  
    }

}
