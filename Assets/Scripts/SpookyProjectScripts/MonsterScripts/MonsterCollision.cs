using UnityEngine;

public class MonsterCollision : MonoBehaviour
{


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player has entered the monster's trigger area.");
            // You can add additional logic here, such as reducing player health or triggering an animation.

            MonsterStateMachine.triggerDoom = true;
            MonsterStateMachine.hallwayDoom = true;

        }
    }
   
  
}
