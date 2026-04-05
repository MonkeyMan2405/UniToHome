using DoorScript;
using UnityEngine;

public class WardrobeTrigger : MonoBehaviour, IInteractable
{

    public Door2 door2Ref;
    public Door3 door3Ref;

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
        }
        else
        { 
            wardrobeCollider.isTrigger = true;
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
            timer += Time.deltaTime;

            if (timer >= 2.5f)
            {
                door2Ref.open = false;
                door3Ref.open = false;
                wardrobeCollider.isTrigger = false;

            }
        }
        else
        {
            timer = 0f;
        }

        Debug.Log(timer);
   
    }

}
