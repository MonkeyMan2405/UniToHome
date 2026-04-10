using DoorScript;
using UnityEngine;

public class DoorTrigger : MonoBehaviour, IInteractable
{
    private Collider doorCollider;
    public Door doorRef;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        doorCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Interact()
    {
        doorRef.Interact();

        if (doorCollider.isTrigger)
        {
            doorCollider.isTrigger = false;
        }
        else
        {
            doorCollider.isTrigger = true;;
        }
    }


}
